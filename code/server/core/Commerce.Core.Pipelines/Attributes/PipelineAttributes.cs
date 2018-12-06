using System;

namespace Commerce.Core.Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class PipelineAttribute : Attribute
    {
        public Type Implementation { get; set; }
    }
    
    [AttributeUsage(AttributeTargets.Class)]
    public class PipelineBlockAttribute : Attribute
    {
        public string Name { get; set; }
        public Type Pipeline { get; set; }
        public int Order { get; set; }
    }
}