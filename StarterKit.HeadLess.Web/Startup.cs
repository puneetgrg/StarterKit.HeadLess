using EPiServer.Cms.Shell;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ContentApi.Core.DependencyInjection;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;

namespace StarterKit.HeadLess.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;
        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_webHostingEnvironment.IsDevelopment())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

                services.Configure<SchedulerOptions>(options => options.Enabled = false);
            }

            services
                .AddCmsAspNetIdentity<ApplicationUser>()
                .AddCms()
                .AddAdminUserRegistration()
                .AddEmbeddedLocalization<Startup>();

            services.AddContentSearchApi(options => {
                options.MaximumSearchResults = 10;
            });
            services.AddContentDeliveryApi(options => {
                options.SiteDefinitionApiEnabled = true;
            });

            //Enable the edit more preview for the headless application
            if (!string.IsNullOrEmpty(_configuration.GetValue<string>("ContentDeliveryApi:ExternalApplicationUrl")))
            {
                services.ConfigureForExternalTemplates();
                services.Configure<ExternalApplicationOptions>(options => options.OptimizeForDelivery = true);
            }

            //Inline blocks disable
            services.Configure<UIOptions>(uiOptions =>
            {
                uiOptions.InlineBlocksInContentAreaEnabled = false;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            //Allow Cross origin for the headless app
            var previewUrl = _configuration.GetValue<string>("ContentDeliveryApi:ExternalApplicationUrl");
            if (!string.IsNullOrEmpty(previewUrl))
            {
                app.UseCors(b => b.WithOrigins(previewUrl));
            }
            else
            {
                app.UseCors();
            }


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();
            });
        }
    }
}
