using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDbBank.Lib;
using MongoDbBank.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MongoDbBank.IntegrationTests
{
    public class MongoDbBankServiceTests
    {
        public ServiceCollection ServiceCollection;
        public ServiceProvider ServiceProvider;
        public IConfiguration Configuration;
        public MongoDbBankOptions Options;
        public IMongoDbBankService Service;

        public MongoDbBankServiceTests()
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
            Options = Configuration.GetSection("MongoDbBank").Get<MongoDbBankOptions>();
            ServiceCollection.Configure<MongoDbBankOptions>(options => Configuration.Bind("MongoDbBank", options));
            ServiceCollection.AddTransient<IMongoDbBankService, MongoDbBankService>();
            ServiceProvider = ServiceCollection.BuildServiceProvider();
            Service = ServiceProvider.GetService<IMongoDbBankService>();
        }

        [Fact]
        public async Task CreateAndDeleteAccountTest()
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "TestAccount1"
            };

            await Service.CreateAccount(account);
            Account result;
            
            result = await Service.GetAccount(account.Name);

            Assert.Equal(result.Name, account.Name);

            await Service.RemoveAccount(account.Name);
            result = await Service.GetAccount(account.Name);

            Assert.Null(result);
        }

        [Fact]
        public async Task CreateModifyDeleteTest()
        {
            Account account = new Account()
            {
                Id = Guid.NewGuid(),
                Name = "TestAccount1"
            };

            await Service.CreateAccount(account);
            Account modify, result;

            modify = await Service.GetAccount(account.Name);

            DateTime utcNow = DateTime.UtcNow;
            int amount = 100;

            modify.Transactions = new List<Transaction>
            {
                new Transaction
                {
                    Timestamp = utcNow,
                    Amount = amount,
                }
            };

            await Service.UpdateAccount(modify);
            result = await Service.GetAccount(modify.Id);
            double? balance = await Service.GetBalance(account.Name);

            Assert.Equal(result.Name, modify.Name);
            Assert.Single(result.Transactions);
            Assert.Equal(utcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"), result.Transactions[0].Timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ"));
            Assert.Equal(amount, result.Transactions[0].Amount);
            Assert.Equal(amount, balance);
            await Service.RemoveAccount(modify.Id);
            result = await Service.GetAccount(modify.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task BankBalanceTest()
        {
            int limit = 10;

            for (int i=0; i<limit; i++)
            {
                var account = new Account
                {
                    Id = Guid.NewGuid(),
                    Name = "Account" + i,
                    Transactions = new List<Transaction>(),
                };

                for (int j=0; j<limit; j++)
                {
                    account.Transactions.Add(new Transaction
                    {
                        Timestamp = DateTime.UtcNow,
                        Amount = (j % 2 != 0) ? j * 100 : (-1) * j * 100
                    });
                }

                await Service.CreateAccount(account);
            }

            double? balance = await Service.GetBankTotalBalance();
            Assert.Equal(5000, balance);

            for (int i = 0; i < limit; i++)
            {
                string name = "Account" + i;
                await Service.RemoveAccount(name);
            }
        }

        [Fact]
        public async Task GetBalanceEmptyTest()
        {
            double? balance = await Service.GetBalance("account1");
            Assert.Null(balance);
        }

        [Fact]
        public async Task ModifyBalanceTest()
        {
            string accountName = "account1";
            int modifier = new Random().Next(-10, 10);
            double amount = modifier * 1000;
            await Service.ModifyBalance(accountName, amount);
            double? balance = await Service.GetBalance(accountName);
            Assert.Equal(amount, balance);
            await Service.RemoveAccount(accountName);
        }
    }
}
