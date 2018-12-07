using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Core.Pipelines.Implementation;

namespace Commerce.Core.Pipelines
{
    #region Pipeline interfaces 

    public interface IPipeline: IPipelineBlock
    {
        IEnumerable<IPipelineBlock> Blocks { get; }
    }

    public interface IPipeline<TInput, TOutput, TContext> : IPipelineBlock<TInput, TOutput, TContext>, IPipeline
    {
       
    }

    #endregion

    public interface IPipelineBlock
    {
        object Run(object input, object context);
    }

    public interface IPipelineBlock<TInput, TOutput, TContext> : IPipelineBlock
    {
        TOutput Run(TInput input, TContext context);
    }
    
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