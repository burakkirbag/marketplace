using System;
using System.Linq;
using System.Net;
using System.Text.Json;
using FluentValidation.AspNetCore;
using Marketplace.Api.Mvc.Filters;
using Marketplace.Configuration;
using Marketplace.Engine;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Marketplace.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static (IEngine, AppConfig) ConfigureApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var appConfig = services.ConfigureStartupConfig<AppConfig>(configuration.GetSection("App"));
            services.ConfigureStartupConfig<MongoDbOptions>(configuration.GetSection("App").GetSection("MongoDb"));
            services.ConfigureStartupConfig<EmailOptions>(configuration.GetSection("App").GetSection("Email"));
            services.ConfigureStartupConfig<KeysOptions>(configuration.GetSection("App").GetSection("Keys"));

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });
            services.AddMvc(options => { options.Filters.Add(new ValidationFilter()); });

            var engine = EngineContext.Create();
            engine.ConfigureServices(services, configuration, appConfig);

            return (engine, appConfig);
        }

        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services,
            IConfiguration configuration)
            where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            var config = new TConfig();

            configuration.Bind(config);

            services.Configure<TConfig>(configuration);

            return config;
        }

        public static void AddAppMvc(this IServiceCollection services)
        {
            var mvcBuilder = services.AddControllers();

            mvcBuilder.AddFluentValidation(configuration =>
            {
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("Marketplace", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);

                configuration.RegisterValidatorsFromAssemblies(assemblies);

                configuration.ImplicitlyValidateChildProperties = true;
            });

            mvcBuilder.AddControllersAsServices();
        }

        public static void AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Marketplace.Api", Version = "v1"});
            });
        }
    }
}