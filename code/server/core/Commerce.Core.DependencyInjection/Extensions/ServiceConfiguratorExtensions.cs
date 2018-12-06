using System;
using System.Collections.Generic;
using System.Linq;
using Commerce.Core.DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Commerce.Core.DependencyInjection.Extensions
{
    public static class ServiceConfiguratorExtensions
    {
        public static IServiceCollection AddServiceConfigurator(this IServiceCollection serviceCollection)
        {
            foreach (var serviceImpl in typeof(IServiceConfigurator).GetAllImplementingClasses())
            {
                serviceCollection.AddSingleton(typeof(IServiceConfigurator), serviceImpl);
            }
            var tempServiceProvider = serviceCollection.BuildServiceProvider();
            var serviceConfigurators = tempServiceProvider.GetServices<IServiceConfigurator>();
            foreach (var serviceConfigurator in serviceConfigurators)
            {
                serviceConfigurator.Configure(serviceCollection);
            }
            return serviceCollection;
        }

        public static IEnumerable<Type> GetAllImplementingClasses(this Type requiredType)
        {
            return requiredType.GetAllImplementingWithFilter(type => !type.IsAbstract);
        }
        
        public static IEnumerable<Type> GetAllImplementingInterfaces(this Type requiredType)
        {
            return requiredType.GetAllImplementingWithFilter(type => type.IsInterface);
        }

        private static IEnumerable<Type> GetAllImplementingWithFilter(this Type requiredType, Func<Type, bool> filter)
        {
            var notServiceAssemblies = DependencyContext.Default.RuntimeLibraries.Where(lib => !lib.Serviceable).ToList();
            foreach (var apiAssembly in notServiceAssemblies)
            {
                var assembly = AppDomain.CurrentDomain.Load(apiAssembly.Name);
                foreach (var exportedType in assembly.GetExportedTypes())
                {
                    if (filter(exportedType) && requiredType.IsAssignableFrom(exportedType))
                    {
                        yield return exportedType;
                    }
                }
            }
        }
    }
}