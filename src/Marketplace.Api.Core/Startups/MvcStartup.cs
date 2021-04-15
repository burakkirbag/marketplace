using Marketplace.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Api.Startups
{
    public class MvcStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppMvc();
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseAppEndpoints();
            application.UseAppCorrelationMiddleware();
        }

        public int Order => 2;
    }
}