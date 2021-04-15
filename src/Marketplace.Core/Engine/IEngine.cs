using System;
using System.Collections.Generic;
using Autofac;
using Marketplace.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Engine
{
    public interface IEngine
    {
        void ConfigureServices(IServiceCollection services, IConfiguration configuration, AppConfig appConfig);

        void ConfigureRequestPipeline(IApplicationBuilder application,IWebHostEnvironment environment);

        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>();

        object ResolveUnregistered(Type type);

        void RegisterDependencies(ContainerBuilder containerBuilder, AppConfig appConfig);
    }
}