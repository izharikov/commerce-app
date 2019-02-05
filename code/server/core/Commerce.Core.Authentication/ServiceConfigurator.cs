using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Commerce.Core.Authentication.Storage;
using Commerce.Core.DependencyInjection.Services;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Commerce.Core.Authentication
{
    public class ServiceConfigurator : IServiceConfigurator
    {
        public ServiceConfigurator(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IServiceCollection services)
        {
            services.AddDbContext<ApplicationUserDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationUserDbContext>()
                .AddDefaultTokenProviders();
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(GetIdentityResources())
                .AddInMemoryApiResources(GetApiResources())
                .AddInMemoryClients(GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            services.Configure<IdentityOptions>(options => { options.ClaimsIdentity = new ClaimsIdentityOptions(); });

            var filename = Path.Combine(Directory.GetCurrentDirectory(), "tempkey.rsa");
            TemporaryRsaKey key = new TemporaryRsaKey();
            if (File.Exists(filename))
            {
                var keyFile = File.ReadAllText(filename);
                key = JsonConvert.DeserializeObject<TemporaryRsaKey>(keyFile,
                    new JsonSerializerSettings {ContractResolver = new RsaKeyContractResolver()});
            }

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(key.Parameters)
                    };
                });
        }

        private class TemporaryRsaKey
        {
            public string KeyId { get; set; }
            public RSAParameters Parameters { get; set; }
        }

        private class RsaKeyContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);

                property.Ignored = false;

                return property;
            }
        }

        private static readonly ICollection<string> UserClaims = new[]
            {JwtClaimTypes.Subject, JwtClaimTypes.Name, ClaimTypes.Role, ClaimTypes.Name};

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("admin_api", UserClaims)
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
                {
                    UserClaims = UserClaims,
                }
            };
        }

        public IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client()
                {
                    ClientId = "admin_site",
                    ClientSecrets = {new Secret(Configuration["Tokens:Key"].Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "admin_api"
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                }
            };
        }
    }
}