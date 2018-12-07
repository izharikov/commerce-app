using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Commerce.Core.Pipelines.Extensions;
using Commerce.Core.Pipelines.Implementation;

namespace Commerce.Core.Pipelines.Models
{
    public class PipelineJsonModel
    {
        public string Name { get; set; }
        public string Receive { get; set; }
        public string Return { get; set; }
        
        public List<PipelineBlockJsonModel> Blocks { get; set; }

        public PipelineJsonModel(IPipeline pipeline)
        {
            Name = pipeline.GetDisplayName();
            Receive = pipeline.GetPipelineGenericArguments()[0].FullName;
            Return = pipeline.GetPipelineGenericArguments()[1].FullName;
            Blocks = pipeline.Blocks.Select(block => new PipelineBlockJsonModel(block)).ToList();
        }
       
    }
}