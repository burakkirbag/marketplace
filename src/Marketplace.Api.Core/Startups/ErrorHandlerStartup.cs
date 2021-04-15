using Marketplace.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Api.Startups
{
    public class ErrorHandlerStartup : IAppStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            application.UseAppExceptionMiddleware();
        }

        public int Order => 0;
    }
}