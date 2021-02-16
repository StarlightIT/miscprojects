using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDbBank.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib
{
    public class MongoDbBankService : IMongoDbBankService
    {
        private readonly ILogger<IMongoDbBankService> _logger;
        private readonly MongoDbBankOptions _options;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Account> _accounts;

        public MongoDbBankService(ILogger<IMongoDbBankService> logger, IOptions<MongoDbBankOptions> options)
        {
            _logger = logger;
            _options = options.Value;

            string connectionString = GetConnectionString();

            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(_options.DatabaseName);
            _accounts = _database.GetCollection<Account>(_options.CollectionName);
        }

        private string GetConnectionString()
        {
            string connectionString = $"mongodb://{_options.Username}:{_options.Passsword}@{_options.ConnectionHost}:{_options.ConnectionPort}/?authSource={_options.AuthDatabase}";
            return connectionString;
        }

        public MongoDbBankOptions Ping()
        {
            return _options;
        }

        public async Task<double?> GetBalance(string accountName)
        {
            var result = await GetAccount(accountName);

            if (result == null)
            {
                return null;
            }

            if (result.CurrentBalance == null)
            {
                await CalculateBalance(result);
            }

            return result.CurrentBalance;
        }

        private async Task CalculateBalance(Account account)
        {

            if (account.Transactions == null || account.Transactions?.Count == 0)
            {
                account.CurrentBalance = 0;
            }
            else
            {
                double balance = GetBalance(account);
                account.CurrentBalance = balance;
            }
            
            await UpdateAccount(account);
        }

        private static double GetBalance(Account account)
        {
            double balance = 0;

            foreach (var transaction in account.Transactions)
            {
                balance += transaction.Amount;
            }

            return balance;
        }

        public async Task<double?> GetBankTotalBalance()
        {
            var accounts = await GetAccounts();

            double balance = 0;

            foreach(var account in accounts)
            {
                balance += GetBalance(account);
            }

            return balance;
        }

        public async Task<bool> ModifyBalance(string accountName, double amount)
        {
            Account account = await GetAccount(accountName);

            if (account == null)
            {
                account = new Account
                {
                    Id = Guid.NewGuid(),
                    Name = accountName,
                };

                await CreateAccount(account);
            }

            if (account.Transactions == null)
            {
                account.Transactions = new List<Transaction>();
            }

            account.Transactions.Add(new Transaction
            {
                Timestamp = DateTime.UtcNow,
                Amount = amount,
            });

            await CalculateBalance(account);

            return true;
        }

        public async Task<List<Account>> GetAccounts()
        {
            var results = await _accounts.FindAsync(accounts => true);
            return results.ToList();
        }

        public async Task<Account> GetAccount(string accountName)
        {
            var account = await _accounts.Find(a => a.Name == accountName).FirstOrDefaultAsync();
            return account;
        }

        public async Task<Account> GetAccount(Guid accountId)
        {
            var account = await _accounts.Find(a => a.Id == accountId).FirstOrDefaultAsync();
            return account;
        }

        public async Task CreateAccount(Account account)
        {
            await _accounts.InsertOneAsync(account);
        }

        public async Task UpdateAccount(Account account)
        {
            var result = await _accounts.ReplaceOneAsync(a => a.Id == account.Id, account);
        }

        public async Task RemoveAccount(string accountName)
        {
            var result = await _accounts.DeleteOneAsync(a => a.Name == accountName);
        }

        public async Task RemoveAccount(Guid accountId)
        {
            var result = await _accounts.DeleteOneAsync(a => a.Id == accountId);
        }
    }
}
