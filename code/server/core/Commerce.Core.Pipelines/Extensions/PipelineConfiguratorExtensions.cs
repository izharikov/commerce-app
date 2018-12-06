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
        public static IServiceCollection AddPipelines(this IServiceCollection serviceCollection)
        {
            var pipelineBlocks = typeof(IPipelineBlock).GetAllImplementingClasses();
            var pipelineToBlockMapping = GeneratePipelineToBlockMapping(pipelineBlocks);
            foreach (var pipelineDefinitionEntry in pipelineToBlockMapping)
            {
                foreach (var pipelineBlock in pipelineDefinitionEntry.Value.Pipelines)
                {
                    serviceCollection.AddSingleton(pipelineBlock);
                }

                serviceCollection.AddSingleton(pipelineDefinitionEntry.Key,
                    provider =>
                    {
                        var implementationObject =
                            (DefaultPipelineImplementation) Activator.CreateInstance(pipelineDefinitionEntry.Value
                                .ImplementationType);
                        implementationObject.Initialize(provider, pipelineDefinitionEntry.Value.Pipelines);
                        return implementationObject;
                    });
                serviceCollection.AddSingleton(typeof(IPipeline),
                    provider =>
                    {
                        var implementationObject =
                            (DefaultPipelineImplementation) Activator.CreateInstance(pipelineDefinitionEntry.Value
                                .ImplementationType);
                        implementationObject.Initialize(provider, pipelineDefinitionEntry.Value.Pipelines);
                        return implementationObject;
                    });
            }

            return serviceCollection;
        }

        private static IDictionary<Type, PipelineReflectionModel> GeneratePipelineToBlockMapping(
            IEnumerable<Type> allBlocks)
        {
            var pipelineToBlockMapping = new Dictionary<Type, PipelineReflectionModel>();
            foreach (var pipelineBlockType in allBlocks)
            {
                if (!pipelineBlockType.TryPipelineBlockInformation(out var info)) continue;
                if (pipelineToBlockMapping.ContainsKey(info.PipelineType))
                {
                    pipelineToBlockMapping[info.PipelineType].PipelineTypes.Add(info);
                }
                else
                {
                    pipelineToBlockMapping[info.PipelineType] = new PipelineReflectionModel()
                    {
                        InterfaceType = info.PipelineType,
                        ImplementationType = info.ImplementationType,
                        PipelineTypes = new List<PipelineBlockReflectionModel>{info}
                    };
                }
            }

            foreach (var value in pipelineToBlockMapping.Values)
            {
                value.SortPipelines();
            }

            return pipelineToBlockMapping;
        }

        private static bool TryPipelineBlockInformation(this Type pipelineBlock,
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
                ImplementationType = attribute.Pipeline.GetCustomAttribute<PipelineAttribute>().Implementation,
                PipelineBlockType = pipelineBlock,
                BlockAttribute = attribute
            };
            return true;
        }
    }

    internal class PipelineReflectionModel
    {
        public Type InterfaceType { get; set; }
        public Type ImplementationType { get; set; }
        public IList<PipelineBlockReflectionModel> PipelineTypes { get; set; }

        public IList<Type> Pipelines => PipelineTypes.Select(m => m.PipelineBlockType).ToList();

        internal void SortPipelines()
        {
            PipelineTypes = PipelineTypes.OrderBy(t => t.BlockAttribute.Order).ToList();
        }
    }

    internal class PipelineBlockReflectionModel
    {
        public Type PipelineType { get; set; }
        public Type ImplementationType { get; set; }
        public Type PipelineBlockType { get; set; }
        public PipelineBlockAttribute BlockAttribute { get; set; }
    }
}