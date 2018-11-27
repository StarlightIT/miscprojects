using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace IoTBackendApi.Tests
{
    public class IntegrationTests
    {
        public ServiceCollection Services;
        public ServiceProvider ServiceProvider;
        public IConfiguration Config;

        [SetUp]
        public void Setup()
        {
            Services = new ServiceCollection();
            ConfigureServices(Services);
            ServiceProvider = Services.BuildServiceProvider();
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables().Build();
            services.AddOptions();
        }
    }
}
