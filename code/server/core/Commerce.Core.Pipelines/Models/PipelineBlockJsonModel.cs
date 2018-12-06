namespace Commerce.Core.Pipelines.Models
{
    public class PipelineBlockJsonModel
    {
        public string ImplementationType { get; set; }
        public string Receive { get; set; }
        public string Return { get; set; }
        
        public PipelineBlockJsonModel Initialize(IPipelineBlock pipeline)
        {
            ImplementationType = pipeline.GetType().AssemblyQualifiedName;
            Receive = pipeline.Receive.FullName;
            Return = pipeline.Return.FullName;
            return this;
        }
    }
}