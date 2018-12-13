using System.Linq;
using Commerce.Core.DependencyInjection.Extensions;
using Commerce.Core.DependencyInjection.Services;
using Commerce.Core.DependencyInjection.Test.Examples;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Commerce.Core.DependencyInjection.Test.Extensions
{
    public class ServiceConfiguratorExtensionsTest
    {
        private IServiceCollection _serviceCollection;

        public ServiceConfiguratorExtensionsTest()
        {
            _serviceCollection = new ServiceCollection();
        }

        [Fact]
        public void Test_ImplementedClasses()
        {
            var implementedClasses = typeof(IImplementedTestInterface).GetAllImplementingInterfacesOrClasses().ToList();
            Assert.Equal(2, implementedClasses.Count);
            Assert.Contains(typeof(RealImplementationClass), implementedClasses);
            Assert.Contains(typeof(IExtendImplementedTestInterface), implementedClasses);
            Assert.DoesNotContain(typeof(IImplementedTestInterface), implementedClasses);
        }

        [Fact]
        public void Test_NotImplementedClasses()
        {
            var implementedClasses = typeof(INotImplementedInterface).GetAllImplementingInterfacesOrClasses().ToList();
            Assert.Empty(implementedClasses); 
        }

        [Fact]
        public void Test_ImplementedInterfaces()
        {
            var implementedClasses = typeof(IImplementedTestInterface).GetAllImplementingInterfaces().ToList();
            Assert.Equal(1, implementedClasses.Count);
            Assert.Contains(typeof(IExtendImplementedTestInterface), implementedClasses);
        }
        
        [Fact]
        public void Test_ServiceConfigurator()
        {
            _serviceCollection.AddServiceConfigurator();
            var serviceConfigurators = _serviceCollection.BuildServiceProvider().GetServices<IServiceConfigurator>();
            var testServiceConfigurator = serviceConfigurators.OfType<TestServiceConfigurator>().FirstOrDefault();
            Assert.NotNull(testServiceConfigurator);
        }
    }
}