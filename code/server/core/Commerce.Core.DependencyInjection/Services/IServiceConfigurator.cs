using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.DependencyInjection.Services
{
    public interface IServiceConfigurator
    {
        void Configure(IServiceCollection services);
    }
}