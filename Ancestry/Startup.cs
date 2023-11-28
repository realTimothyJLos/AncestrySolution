using Ancestry.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ancestry
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string filePath = @"C:\Users\timot\Downloads\Master Family Tree.ged";
            services.AddSingleton<GEDCOMDataRepository>(_ => new GEDCOMDataRepository(filePath));
            services.AddControllersWithViews(); 
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
