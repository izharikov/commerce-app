using System;
using System.Linq;
using Commerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Commerce.Server.Extensions
{
    public static class ServiceConfiguratorExtensions
    {
        public static IServiceCollection AddServiceConfigurator(this IServiceCollection serviceCollection)
        {
            var apiAssemblies = DependencyContext.Default.RuntimeLibraries.Where(lib => !lib.Serviceable).ToList();
            foreach (var apiAssembly in apiAssemblies)
            {
                var assembly = AppDomain.CurrentDomain.Load(apiAssembly.Name);
                foreach (var exportedType in assembly.GetExportedTypes())
                {
                    if (!exportedType.IsAbstract && typeof(IServiceConfigurator).IsAssignableFrom(exportedType))
                    {
                        serviceCollection.AddSingleton(typeof(IServiceConfigurator), exportedType);
                    }
                }
            }
            var tempServiceProvider = serviceCollection.BuildServiceProvider();
            var serviceConfigurators = tempServiceProvider.GetServices<IServiceConfigurator>();
            foreach (var serviceConfigurator in serviceConfigurators)
            {
                serviceConfigurator.Configure(serviceCollection);
            }
            return serviceCollection;
        }
    }
}