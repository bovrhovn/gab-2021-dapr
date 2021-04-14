using Building_Blocks_Web.Hubs;
using Building_Blocks_Web.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Building_Blocks_Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<WebSettings>(Configuration);
            services.Configure<SignalrOptions>(Configuration.GetSection("SignalrOptions"));
            services.AddSignalR().AddAzureSignalR();

            services.AddRazorPages().AddRazorPagesOptions(options =>
                options.Conventions.AddPageRoute("/Info/Index", ""));

            services.AddControllers().AddDapr();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/Error");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseCloudEvents();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapSubscribeHandler();
                endpoints.MapHub<AlertHub>("/alerts");
            });
        }
    }
}