using ShoppingProject.Data;
using ShoppingProject.Domain.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingProject.Web
{
    public static class IdentityRegister
    {
        public static IServiceCollection Register(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(
              options =>
              {
                  options.Password.RequireDigit = false;
                  options.Password.RequiredLength = 6;
                  options.Password.RequireLowercase = false;
                  options.Password.RequireUppercase = false;
                  options.Password.RequireNonAlphanumeric = false;
              }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(
                options =>
                {
                    options.LoginPath = $"/Login";
                    options.AccessDeniedPath = $"/AccessDenied/";
                    options.LogoutPath = $"/Logout/";
                });
            return services;
        }
    }
}
