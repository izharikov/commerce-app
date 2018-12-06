using Commerce.Core.DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Commerce.Server.CatalogApi
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
            _logger.LogInformation("Invoke IServiceConfigurator.Configure");
        }
    }
}