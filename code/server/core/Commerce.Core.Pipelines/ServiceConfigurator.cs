using Commerce.Core.DependencyInjection.Services;
using Commerce.Core.Pipelines.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Pipelines
{
    public class ServiceConfigurator : IServiceConfigurator
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IPipelineService, PipelineService>();
        }
    }
}