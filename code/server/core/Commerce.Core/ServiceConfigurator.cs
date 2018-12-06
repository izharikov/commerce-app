using Commerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Commerce.Core
{
    public class ServiceConfigurator : IServiceConfigurator
    {
        private readonly ILogger<ServiceConfigurator> _logger;

        public ServiceConfigurator(ILogger<ServiceConfigurator> logger)
        {
            _logger = logger;
        }
        
        public void Configure(IServiceCollection services)
        {
            _logger.LogInformation("Commerce.Core.ServiceConfigurtor");
        }
    }
}