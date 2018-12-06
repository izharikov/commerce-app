using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Services
{
    public interface IServiceConfigurator
    {
        void Configure(IServiceCollection services);
    }
}