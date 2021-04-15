using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Marketplace.Api.Middleware;
using Marketplace.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Marketplace.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureRequestPipeline(this IApplicationBuilder application, IWebHostEnvironment environment)
        {
            EngineContext.Current.ConfigureRequestPipeline(application, environment);
        }

        public static void UseAppCorrelationMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<CorrelationMiddleware>();
        }

        public static void UseAppExceptionMiddleware(this IApplicationBuilder application)
        {
            application.UseMiddleware<ExceptionMiddleware>();
        }

        public static void UseAppEndpoints(this IApplicationBuilder application)
        {
            application.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public static void UseAppSwagger(this IApplicationBuilder application)
        {
            application.UseSwagger();
            application.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketplace.Api v1"));
        }
    }
}