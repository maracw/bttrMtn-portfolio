using System;
using ButterMtn_296.Models;
using Microsoft.AspNetCore.Identity;

namespace ButterMountain2.Data
{
	public class SeedUsers
	{
        private static RoleManager<IdentityRole> roleManager;
        private static UserManager<AppUser> userManager;

        public static async Task CreateUsersAsync(IServiceProvider provider)
        {
            roleManager =
                provider.GetRequiredService<RoleManager<IdentityRole>>();
            userManager =
                provider.GetRequiredService<UserManager<AppUser>>();
            const string MEMBER = "Member";
            await CreateRole(MEMBER);
            const string ADMIN = "admin";
            await CreateRole(ADMIN);

            const string PASSWORD = "Sesame123!";

            await CreateUser("Mara", PASSWORD, MEMBER);
            await CreateUser("Figgy", PASSWORD, MEMBER);
            await CreateUser("admin", PASSWORD, ADMIN);
            await CreateUser("OrphanAccount", PASSWORD, ADMIN);
        }
        public static async Task CreateRole(string roleName)
        {
            if (await roleManager.FindByNameAsync(roleName) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        public static async Task CreateUser(string username, string password, string role)
        {
            // if username doesn't exist, create it and add to role
            if (await userManager.FindByNameAsync(username) == null)
            {
                AppUser user = new AppUser { UserName = username };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
    }
}

