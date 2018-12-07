using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Commerce.Core.DependencyInjection.Extensions;
using Commerce.Core.Pipelines.Attributes;
using Commerce.Core.Pipelines.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Pipelines.Extensions
{
    public static class PipelineConfiguratorExtensions
    {
        /// <summary>
        /// add all classes with <see cref="PipelineAttribute"/> as pipelines with associated blocks, marked with <see cref="PipelineBlockAttribute"/>
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipelinesFromAttributes(this IServiceCollection serviceCollection)
        {
            var pipelineBlocks = typeof(IPipelineBlock).GetAllImplementingInterfacesOrClasses();
            var allPipelineDefinitions = GeneratePipelineDefinitions(pipelineBlocks);
            foreach (var pipelineDefinition in allPipelineDefinitions)
            {
                serviceCollection.ConfigurePipeline(pipelineDefinition);
            }

            return serviceCollection;
        }

        /// <summary>
        /// Configure single pipeline definition
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="pipelineReflectionDetails"></param>
        private static void ConfigurePipeline(this IServiceCollection serviceCollection,
            PipelineReflectionModel pipelineReflectionDetails)
        {
            // first, register all pipeline blocks, that are classes
            foreach (var pipelineBlock in pipelineReflectionDetails.PipelineBlocks)
            {
                if (pipelineBlock.IsClass && !pipelineBlock.IsAbstract)
                {
                    serviceCollection.AddSingleton(pipelineBlock);
                }
            }

            object pipelineImplementationObject = null;

            // inline function to generate pipeline class based on provided pipeline definition
            object PipelineFactory(IServiceProvider provider)
            {
                if (pipelineImplementationObject != null)
                {
                    return pipelineImplementationObject;
                }

                // create pipeline object with reflection
                pipelineImplementationObject = Activator.CreateInstance(pipelineReflectionDetails.ImplementationType);
                // initialize pipeline with blocks
                ((IPipelineInitializer) pipelineImplementationObject).Initialize(provider,
                    pipelineReflectionDetails.PipelineBlocks);
                return pipelineImplementationObject;
            }

            // add in DI to associate with pipeline interface using factory
            serviceCollection.AddSingleton(pipelineReflectionDetails.InterfaceType, PipelineFactory);
            // add the same factory to IPipeline interface for service purpose
            serviceCollection.AddSingleton(typeof(IPipeline), PipelineFactory);
        }

        /// <summary>
        /// scan all classpath to find any classes with <see cref="IPipeline{TInput,TOutput,TContext}"/>
        /// and <see cref="IPipelineBlock{TInput,TOutput,TContext}"/> definition and map blocks to pipeline
        /// </summary>
        /// <param name="allBlockTypes"></param>
        /// <returns></returns>
        private static IEnumerable<PipelineReflectionModel> GeneratePipelineDefinitions(
            IEnumerable<Type> allBlockTypes)
        {
            var pipelineToBlockMapping = new Dictionary<Type, PipelineReflectionModel>();
            foreach (var pipelineBlockType in allBlockTypes)
            {
                if (!pipelineBlockType.TryParsePipelineBlockInformation(out var info)) continue;
                if (pipelineToBlockMapping.ContainsKey(info.PipelineType))
                {
                    pipelineToBlockMapping[info.PipelineType].PipelineTypes.Add(info);
                }
                else
                {
                    pipelineToBlockMapping[info.PipelineType] = new PipelineReflectionModel()
                    {
                        InterfaceType = info.PipelineType,
                        ImplementationType = info.BlockAttribute.Pipeline.GetCustomAttribute<PipelineAttribute>()
                            .Implementation,
                        PipelineTypes = new List<PipelineBlockReflectionModel> {info}
                    };
                }
            }

            foreach (var value in pipelineToBlockMapping.Values)
            {
                value.SortPipelines();
            }

            return pipelineToBlockMapping.Values;
        }

        /// <summary>
        /// parse block's information from linked attribute
        /// </summary>
        /// <param name="pipelineBlock"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private static bool TryParsePipelineBlockInformation(this Type pipelineBlock,
            out PipelineBlockReflectionModel result)
        {
            result = null;
            if (!typeof(IPipelineBlock).IsAssignableFrom(pipelineBlock))
            {
                return false;
            }

            var attribute = pipelineBlock.GetCustomAttribute<PipelineBlockAttribute>();
            if (attribute == null)
            {
                return false;
            }

            result = new PipelineBlockReflectionModel
            {
                PipelineType = attribute.Pipeline,
                PipelineBlockType = pipelineBlock,
                BlockAttribute = attribute
            };
            return true;
        }
    }

    public class PipelineReflectionModel
    {
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        public IList<PipelineBlockReflectionModel> PipelineTypes { get; set; }

        public IList<Type> PipelineBlocks => PipelineTypes.Select(m => m.PipelineBlockType).ToList();

        internal void SortPipelines()
        {
            PipelineTypes = PipelineTypes.OrderBy(t => t.BlockAttribute.Order).ToList();
        }
    }

    public class PipelineBlockReflectionModel
    {
        public Type PipelineType { get; set; }
        public Type PipelineBlockType { get; set; }
        public PipelineBlockAttribute BlockAttribute { get; set; }
    }
}