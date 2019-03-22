using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IoTBackendApi.Models.Configuration;
using IoTBackendApi.Services;
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
        public StorageOptions _storageOptions;
        public IIotDataService _iotDataService;

        [SetUp]
        public void Setup()
        {
            Services = new ServiceCollection();
            ConfigureServices(Services);
            ServiceProvider = Services.BuildServiceProvider();
            _iotDataService = ServiceProvider.GetService<IIotDataService>();
            
        }

        protected void ConfigureServices(IServiceCollection services)
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables().Build();
            services.AddOptions();
            _storageOptions = Config.GetSection("StorageOptions").Get<StorageOptions>();

            services.Configure<StorageOptions>(options => Config.Bind("StorageOptions", options));
            services.AddSingleton<IStorageService, BlobStorageService>();
            services.AddTransient<IIotDataService, IotDataService>();
        }
    }
}
