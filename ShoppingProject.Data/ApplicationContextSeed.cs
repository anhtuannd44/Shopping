using Microsoft.AspNetCore.Identity;
using ShoppingProject.Data.Constants;
using ShoppingProject.Domain.DomainModels;
using System;
using System.Threading.Tasks;

namespace ShoppingProject.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));

            var defaultUser = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            await userManager.CreateAsync(defaultUser, AuthorizationConstants.DEFAULT_PASSWORD);

            string adminUserName = "admin@admin.com";
            var adminUser = new ApplicationUser { UserName = adminUserName, Email = adminUserName, DateCreated = DateTime.Now };
            await userManager.CreateAsync(adminUser, AuthorizationConstants.DEFAULT_PASSWORD);
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
