using Microsoft.AspNetCore.Identity;

namespace Commerce.Core.Authentication
{
    public static class Constants
    {
        public static class Roles
        {
            public static readonly IdentityRole Admin = new IdentityRole
            {
                Id = "role-1",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper()
            };

            public static readonly IdentityRole CommerceUser = new IdentityRole
            {
                Id = "role-2",
                Name = "CommerceUser",
                NormalizedName = "CommerceUser".ToUpper()
            };

            public static readonly IdentityRole Developer = new IdentityRole
            {
                Id = "role-3",
                Name = "Developer", 
                NormalizedName = "Developer".ToUpper()
            };

            public static readonly IdentityRole ContentManager = new IdentityRole
            {
                Id = "role-4",
                Name = "ContentManager",
                NormalizedName = "ContentManager".ToUpper()
            };
        }
    }
}