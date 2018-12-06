using System.Collections.Generic;
using Commerce.Core.Pipelines.Models;

namespace Commerce.Core.Pipelines.Service
{
    public interface IPipelineService
    {
        List<PipelineJsonModel> GetRegisteredPipelinesJson();
    }
}