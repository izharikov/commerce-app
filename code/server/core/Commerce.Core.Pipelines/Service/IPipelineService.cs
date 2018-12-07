using System.Collections.Generic;
using Commerce.Core.Pipelines.Models;

namespace Commerce.Core.Pipelines.Service
{
    /// <summary>
    /// system service to get pipeline information
    /// </summary>
    public interface IPipelineService
    {
        List<PipelineJsonModel> GetRegisteredPipelinesJson();
    }
}