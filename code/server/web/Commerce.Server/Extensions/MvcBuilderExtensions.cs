using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;

namespace Commerce.Server.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static readonly Regex ApiAssemblyRegex = new Regex(@"^Commerce.*Api$");

        public static IMvcBuilder AddApiAssemblies(this IMvcBuilder mvcBuilder)
        {
            var apiAssemblies = DependencyContext.Default.RuntimeLibraries
                .Where(assembly => ApiAssemblyRegex.IsMatch(assembly.Name)).ToList();
            foreach (var apiAssembly in apiAssemblies)
            {
                var assembly = AppDomain.CurrentDomain.Load(apiAssembly.Name);
                mvcBuilder.AddApplicationPart(assembly).AddControllersAsServices();
            }

            return mvcBuilder;
        }
    }
}