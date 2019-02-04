using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Commerce.Core.Authentication.Storage
{
    public class ApplicationDbInitializer
    {
        public static async Task InitialDataSeed(IConfiguration configuration, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            await SeedRoles(configuration, roleManager);
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            await SeedUsers(configuration, userManager);
        }

        private static async Task SeedRoles(IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            var shouldFillRoles = configuration.GetValue<bool>("Authentication:FillRoles");
            if (!shouldFillRoles)
            {
                return;
            }

            var roles = configuration.GetSection("Authentication:Roles").Get<List<string>>();
            foreach (var role in roles)
            {
                if (await roleManager.FindByNameAsync(role) != null) continue;
                await roleManager.CreateAsync(new IdentityRole()
                {
                    Name = role,
                    NormalizedName = role.ToUpperInvariant()
                });
            }
        }

        private static async Task SeedUsers(IConfiguration configuration, UserManager<ApplicationUser> userManager)
        {
            var shouldFillUsers = configuration.GetValue<bool>("Authentication:FillUsers");
            if (!shouldFillUsers)
            {
                return;
            }

            var users = configuration.GetSection("Authentication:Users").GetChildren();
            foreach (var user in users)
            {
                var username = user["username"];
                var userInDb = await userManager.FindByEmailAsync(username);
                var shouldCreateUser = true;
                var shouldSetRoles = true;
                if (userInDb != null)
                {
                    shouldCreateUser = false;
                    var roles = await userManager.GetRolesAsync(userInDb);
                    shouldSetRoles = !roles.Any();
                }

                if (shouldCreateUser)
                {
                    userInDb = new ApplicationUser
                    {
                        UserName = username,
                        Email = username
                    };
                    if (!(await userManager.CreateAsync(userInDb, user["password"])).Succeeded)
                    {
                        userInDb = null;
                    }
                }

                if (userInDb != null && shouldSetRoles)
                {
                    await userManager.AddToRolesAsync(userInDb, user.GetSection("roles").Get<List<string>>());
                }
            }
        }
    }
}