using MongoDbBank.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbBank.Lib
{
    public interface IMongoDbBankService
    {
        /// <summary>
        /// Debug function to test the depenency injection system
        /// </summary>
        /// <returns></returns>
        public MongoDbBankOptions Ping();

        Task<double?> GetBalance(string accountName);
        Task<bool> ModifyBalance(string accountName, double amount);
        Task<double?> GetBankTotalBalance();

        public Task<List<Account>> GetAccounts();
        public Task<Account> GetAccount(string accountName);
        public Task<Account> GetAccount(Guid accountId);
        public Task CreateAccount(Account account);
        public Task UpdateAccount(Account Account);
        public Task RemoveAccount(string accountName);
        public Task RemoveAccount(Guid accountId);
    }
}
