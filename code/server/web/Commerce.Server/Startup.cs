﻿using System.Threading.Tasks;
using Commerce.Core.Authentication;
using Commerce.Core.Authentication.Middleware;
using Commerce.Core.Authentication.Storage;
using Commerce.Core.DependencyInjection.Extensions;
using Commerce.Core.Mvc.Extensions;
using Commerce.Core.Pipelines.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddApiAssemblies()
                .AddAssembly("Commerce.Core.Authentication")
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services
                .AddServiceConfigurator()
                .AddPipelinesFromAttributes();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseMiddleware<CommonAuthenticationMiddleware>();
            using (var scope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbInitializer.InitialDataSeed(Configuration,
                    scope.ServiceProvider).Wait();
            }

            app.UseMvc();
        }
    }
}