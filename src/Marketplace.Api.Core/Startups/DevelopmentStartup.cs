using Marketplace.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Marketplace.Api.Startups
{
    public class DevelopmentStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppSwagger();
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseAppSwagger();
        }

        public int Order => 3;
    }
}