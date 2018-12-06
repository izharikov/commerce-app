using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Core.Pipelines.Implementation;

namespace Commerce.Core.Pipelines
{
    #region Pipeline interfaces 

    public interface IPipeline
    {
        IEnumerable<IPipelineBlock> Blocks { get; }
        object Run(object input, object context);
        Type Receive { get; }
        Type Return { get; }
    }

    public interface IPipeline<TInput, TOutput, TContext> : IPipeline
    {
        TOutput Run(TInput input, TContext context);
    }

    #endregion

    public interface IPipelineBlock : IPipeline
    {
    }

    public interface IPipelineBlock<TInput, TOutput, TContext> : IPipeline<TInput, TOutput, TContext>, IPipelineBlock
    {
    }

    public abstract class DefaultPipeline<TInput, TOutput, TContext> : DefaultPipelineImplementation,
        IPipeline<TInput, TOutput, TContext>
    {
        public TOutput Run(TInput input, TContext context)
        {
            return (TOutput) base.Run(input, context);
        }

        public new Type Receive => typeof(TInput);
        public new Type Return => typeof(TOutput);
    }

    public abstract class DefaultPipelineBlock<TInput, TOutput, TContext> : IPipelineBlock<TInput, TOutput, TContext>
    {
        public abstract TOutput Run(TInput arg, TContext context);

        public IEnumerable<IPipelineBlock> Blocks => Enumerable.Empty<IPipelineBlock>();

        public object Run(object arg, object context)
        {
            return Run((TInput) arg, (TContext) context);
        }

        public Type Receive => typeof(TInput);
        public Type Return => typeof(TOutput);
    }
}