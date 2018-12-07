using System;
using System.Linq;

namespace Commerce.Core.Pipelines.Extensions
{
    internal static class PipelineReflectionExtensions
    {
        internal static Type[] GetPipelineGenericArguments(this IPipeline pipeline)
        {
            return pipeline.GetType().GetPipelineGenericArguments();
        }
        
        internal static Type[] GetPipelineGenericArguments(this Type pipelineType)
        {
            return pipelineType.GetInterfaces().FirstOrDefault(inter => inter.IsGenericType && typeof(IPipeline).IsAssignableFrom(inter))
                ?.GetGenericArguments();
        }
        
        internal static Type[] GetPipelineBlockGenericArguments(this IPipelineBlock pipelineBlock)
        {
            return pipelineBlock.GetType().GetPipelineBlockGenericArguments();
        } 
        
        internal static Type[] GetPipelineBlockGenericArguments(this Type pipelineBlockType)
        {
            return pipelineBlockType.GetInterfaces().FirstOrDefault(inter => inter.IsGenericType && typeof(IPipelineBlock).IsAssignableFrom(inter))
                ?.GetGenericArguments();
        }

        internal static string GetDisplayName(this IPipelineBlock pipelineBlock)
        {
            // it's pipeline block, not pipeline
            if (!(pipelineBlock is IPipeline))
            {
                return pipelineBlock.GetType().FullName;
            }
            // here is pipeline
            return pipelineBlock.GetType().GetInterfaces().FirstOrDefault(inter => 
                !inter.IsGenericType && typeof(IPipelineBlock) != inter && typeof(IPipeline) != inter && typeof(IPipeline).IsAssignableFrom(inter))
                ?.FullName;
        }

    }
}