using System;

namespace Commerce.Core.Pipelines.Attributes
{
    /// <summary>
    /// define pipeline. <br></br>
    /// Pipeline represents 
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class PipelineAttribute : Attribute
    {
        public Type Implementation { get; private set; }

        public PipelineAttribute(Type implementation)
        {
            Implementation = implementation;
        }
    }
    
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class PipelineBlockAttribute : Attribute
    {
        public Type Pipeline { get; set; }
        public int Order { get; set; }
    }
}