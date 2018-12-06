using System.Collections.Generic;
using System.Linq;

namespace Commerce.Core.Pipelines.Models
{
    public class PipelineJsonModel
    {
        public string InterfaceType { get; set; }
        public string ImplementationType { get; set; }
        public string Receive { get; set; }
        public string Return { get; set; }
        
        public List<PipelineBlockJsonModel> Blocks { get; set; }

        public PipelineJsonModel Initialize(IPipeline pipeline)
        {
            ImplementationType = pipeline.GetType().FullName;
            Receive = pipeline.Receive.FullName;
            Return = pipeline.Return.FullName;
            Blocks = pipeline.Blocks.Select(block => new PipelineBlockJsonModel().Initialize(block)).ToList();
            return this;
        }
    }
}