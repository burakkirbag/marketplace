using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Api.Startups
{
    public class CommonStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddRouting();
            services.AddCors();
            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseStaticFiles();
            application.UseRouting();
        }

        public int Order => 1;
    }
}