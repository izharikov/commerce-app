using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Core.Pipelines.Implementation;

namespace Commerce.Core.Pipelines
{
    
    public abstract class DefaultPipeline<TInput, TOutput, TContext> : DefaultPipelineImplementation<TInput, TOutput, TContext>
    {
    }

    public abstract class DefaultPipelineBlock<TInput, TOutput, TContext> : IPipelineBlock<TInput, TOutput, TContext>
    {
        public abstract TOutput Run(TInput arg, TContext context);

        public IEnumerable<IPipelineBlock> Blocks => Enumerable.Empty<IPipelineBlock>();

        public object Run(object arg, object context)
        {
            return Run((TInput) arg, (TContext) context);
        }
    }
}