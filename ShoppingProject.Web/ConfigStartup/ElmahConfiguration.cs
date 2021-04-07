using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingProject.Web
{
    public static class ElmahConfig
    {
        public static IApplicationBuilder UseElmah(IApplicationBuilder app)
        {
            return app.UseElmah();
        }
    }
    public static class AddElmah
    {
        public static IServiceCollection AddService(IServiceCollection services, IConfiguration Configuration)
        {
            return services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("MSSqlConnection");
            });
        }
    }
}
