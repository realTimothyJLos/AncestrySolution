using Ancestry.Repository;
using Ancestry.Utilities.Constraints;
using Microsoft.Extensions.Configuration;

namespace Ancestry
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string filePath = Configuration["FilePaths:GEDCOMFilePath"];
            services.AddSingleton<GEDCOMDataRepository>(_ => new GEDCOMDataRepository(filePath));
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "individualDetails",
                    pattern: "Individual/Details/{individualId}",
                    defaults: new { controller = "Individual", action = "Details" },
                    constraints: new { individualId = new GEDCOMIdConstraint() }
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
