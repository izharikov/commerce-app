using System.Collections.Generic;

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
}