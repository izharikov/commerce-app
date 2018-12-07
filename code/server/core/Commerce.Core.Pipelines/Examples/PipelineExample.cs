using Commerce.Core.Pipelines.Attributes;

namespace Commerce.Core.Pipelines.Examples
{
    [Pipeline(typeof(PipelineExample))]
    public interface IPipelineExample : IPipeline<string, int, object>
    {
    }

    public class PipelineExample : DefaultPipeline<string, int, object>, IPipelineExample
    {
    }

    [PipelineBlock(Order = 0, Pipeline = typeof(IPipelineExample))]
    public class PipelineBlockSecond : DefaultPipelineBlock<string, int, object>
    {
        public override int Run(string arg, object context)
        {
            return arg.Length;
        }
    }

    [PipelineBlock(Order = -1, Pipeline = typeof(IPipelineExample))]
    public class PipelineBlockFirst : DefaultPipelineBlock<string, string, object>
    {
        public override string Run(string arg, object context)
        {
            return arg + arg;
        }
    }

    [Pipeline(typeof(PipelineExampleAsBlock))]
    [PipelineBlock(Order = 3, Pipeline = typeof(IPipelineExample))]
    public interface IPipelineExampleAsBlock : IPipeline<int, int, object>
    {
    }

    public class PipelineExampleAsBlock : DefaultPipeline<int, int, object>, IPipelineExampleAsBlock
    {
    }

    [PipelineBlock(Order = 0, Pipeline = typeof(IPipelineExampleAsBlock))]
    public class PipelineBlockDoubleFirst : DefaultPipelineBlock<int, int, object>
    {
        public override int Run(int arg, object context)
        {
            return arg + arg;
        }
    }
}