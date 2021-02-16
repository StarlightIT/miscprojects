using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDbBank.Lib;
using MongoDbBank.Lib.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MongoDbApp.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
            {
                services.Configure<MongoDbBankOptions>(hostContext.Configuration.GetSection("MongoDbBank"));
                services.AddSingleton<IMongoDbBankService, MongoDbBankService>();
                services.AddHostedService<MongoDbBankHostedService>();
            });
        }
        
    }
}
