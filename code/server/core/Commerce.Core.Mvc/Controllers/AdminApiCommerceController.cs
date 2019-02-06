using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;

namespace Commerce.Core.Mvc.Controllers
{
    [Authorize(Roles = Constants.Roles.Admin)]
    public abstract class AdminApiCommerceController : ApiCommerceController
    {
        
    }
}