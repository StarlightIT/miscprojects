using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace MongoDbBank
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
            .RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            Host.CreateDefaultBuilder(args);
        }
    }
}
