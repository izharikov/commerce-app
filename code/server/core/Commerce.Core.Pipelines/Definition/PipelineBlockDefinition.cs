namespace Commerce.Core.Pipelines
{
    public interface IPipelineBlock
    {
        object Run(object input, object context);
    }

    public interface IPipelineBlock<TInput, TOutput, TContext> : IPipelineBlock
    {
        TOutput Run(TInput input, TContext context);
    }
}