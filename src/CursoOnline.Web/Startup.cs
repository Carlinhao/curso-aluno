using CurosOnline.Dominio._Base;
using CursoOnline.Ioc;
using CursoOnline.Ioc.SwaggerConfiguration;
using CursoOnline.Web.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CursoOnline.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddControllers();
            StartUpIoc.ConfigureServices(services, Configuration);
            services.AddMvc(config => {
                config.Filters.Add(typeof(CustonExceptionFilter));
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();

                var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
                await unitOfWork.Commit();
            });

            app.SwaggerConfigure();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}