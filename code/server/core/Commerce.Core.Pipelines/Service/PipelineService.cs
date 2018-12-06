using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Core.Pipelines.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Pipelines.Service
{
    public class PipelineService : IPipelineService
    {
        private readonly IServiceProvider _serviceProvider;
        
        public PipelineService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public List<PipelineJsonModel> GetRegisteredPipelinesJson()
        {
            return _serviceProvider.GetServices<IPipeline>().Select(pipeline => new PipelineJsonModel().Initialize(pipeline)).ToList();
        }
    }
}