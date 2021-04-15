using System;
using System.IO;
using System.Net.Http;
using Autofac.Extensions.DependencyInjection;
using Bogus;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Marketplace.Test.Core
{
    public abstract class IntegrationTestBase<TStartup> where TStartup : class
    {
        protected readonly IHost Host;
        protected Faker Faker = new Faker();

        private HttpClient _client;
        protected HttpClient Client => _client ?? Host.GetTestClient();

        public IntegrationTestBase()
        {
            Host = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.Development.json");
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<TStartup>();
                }).Build();
        }
    }
}