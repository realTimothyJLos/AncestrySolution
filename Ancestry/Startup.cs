using Ancestry.Parsing;
using Ancestry.Repository;

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
            services.AddScoped<GEDCOMParser>();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseStaticFiles(); // This middleware serves static files from wwwroot folder


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "individualDetails",
                    pattern: "Individual/Details/{individualId}",
                    defaults: new { controller = "Individual", action = "Details" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }



    }
}
