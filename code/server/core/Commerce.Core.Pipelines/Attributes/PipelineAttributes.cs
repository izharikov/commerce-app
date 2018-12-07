using System;

namespace Commerce.Core.Pipelines.Attributes
{
    [AttributeUsage(AttributeTargets.Interface)]
    public class PipelineAttribute : Attribute
    {
        public Type Implementation { get; set; }

        public PipelineAttribute(Type implementation)
        {
            Implementation = implementation;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class PipelineBlockAttribute : Attribute
    {
        public string Name { get; set; }
        public Type Pipeline { get; set; }
        public int Order { get; set; }
    }
}