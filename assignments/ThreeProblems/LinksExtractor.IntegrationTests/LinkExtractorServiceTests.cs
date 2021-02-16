using LinksExtractor.Lib;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Xunit;

namespace LinksExtractor.UnitTests
{
    public class LinkExtractorServiceTests
    {
        public ServiceCollection ServiceCollection;
        public ServiceProvider ServiceProvider;
        public IConfiguration Configuration;
        public LinksExtractorService Service;
        
        public LinkExtractorServiceTests()
        {
            Configuration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", false, true)
                 .AddEnvironmentVariables()
                 .AddUserSecrets(Assembly.GetExecutingAssembly(), false)
                 .Build();
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddLogging(configure => {
                configure.AddConfiguration(Configuration.GetSection("Logging"));
                configure.AddDebug();
                configure.AddConsole();
            });
            ServiceCollection.AddTransient<LinksExtractorService>();
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            Service = ServiceProvider.GetService<LinksExtractorService>();
        }

        [Fact]
        public void GoogleTest()
        {
            var links = Service.ExtractLinks("https://www.google.com/");
            Assert.NotEmpty(links);
        }
    }
}
