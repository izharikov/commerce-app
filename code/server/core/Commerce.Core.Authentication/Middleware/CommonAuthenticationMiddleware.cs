using System.Threading.Tasks;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Commerce.Core.Authentication.Middleware
{
    public class CommonAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAuthenticationSchemeProvider _authenticationSchemeProvider;

        public CommonAuthenticationMiddleware(RequestDelegate next,
            IAuthenticationSchemeProvider authenticationSchemeProvider)
        {
            _next = next;
            _authenticationSchemeProvider = authenticationSchemeProvider;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User == null || !context.User.IsAuthenticated())
            {
                var schemes = await _authenticationSchemeProvider.GetAllSchemesAsync();
                foreach (var authenticationScheme in schemes)
                {
                    var authenticateResult = await context.AuthenticateAsync(authenticationScheme.Name);
                    if (authenticateResult?.Principal != null)
                    {
                        context.User = authenticateResult.Principal;
                        break;
                    }
                }
            }

            await _next(context);
        }
    }
}