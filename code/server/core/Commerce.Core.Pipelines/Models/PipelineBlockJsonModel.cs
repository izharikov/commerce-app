using Commerce.Core.Pipelines.Extensions;

namespace Commerce.Core.Pipelines.Models
{
    public class PipelineBlockJsonModel
    {
        public string Name { get; set; }
        public string Receive { get; set; }
        public string Return { get; set; }

        public PipelineBlockJsonModel(IPipelineBlock pipeline)
        {
            Name = pipeline.GetDisplayName();
            Receive = pipeline.GetPipelineBlockGenericArguments()[0].FullName;
            Return = pipeline.GetPipelineBlockGenericArguments()[1].FullName;
        }
    }
}