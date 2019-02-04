using Commerce.Core.Mvc.Controllers;
using Microsoft.Extensions.Configuration;

namespace Commerce.Core.Authentication.Controllers
{
    public class JwtAuthenticationController : ApiCommerceController
    {
        public JwtAuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }
}