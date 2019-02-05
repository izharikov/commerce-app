using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Commerce.Core.Mvc.Controllers;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Commerce.Core.Authentication.Controllers
{
    public class UserInformationController : ApiCommerceController
    {
        public UserInformationController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; set; }

        [HttpGet]
        [Route("info")]
        [Authorize(AuthenticationSchemes = IdentityServerAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Information()
        {
            var currentUser = await UserManager.GetUserAsync(User);
            if (currentUser != null)
            {
                return new ObjectResult(new
                {
                    UserManagementInfo = new
                    {
                        currentUser.UserName,
                        currentUser.Id,
                        Roles = await UserManager.GetRolesAsync(currentUser)
                    },
                    ContextUser = new
                    {
                        UserName = UserManager.GetUserName(User),
                        UserId = UserManager.GetUserId(User),
                        Roles = User.Claims
                            .Where(claim => claim.Type == UserManager.Options.ClaimsIdentity.RoleClaimType)
                            .Select(c => c.Value)
                    }
                });
            }

            return new NotFoundObjectResult(new
            {
                Reason = "User Not Authorized."
            });
        }

        [HttpGet]
        public async Task<IActionResult> AllUsers()
        {
            return new ObjectResult(UserManager.Users.ToList().Select(u => new
            {
                u.UserName,
                u.Id
            }));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> UserInfo(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return new ObjectResult(new
            {
                user.UserName,
                user.Id
            });
        }


        [Route("test")]
        [HttpGet]
        public IActionResult Test()
        {
            return new ObjectResult(new
            {
                str = "dafdsadsa"
            });
        }
    }
}