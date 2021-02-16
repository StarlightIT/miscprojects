using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDbBank.Lib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDbBank.Lib
{
    public class MongoDbBankHostedService : IHostedService
    {
        private readonly ILogger<MongoDbBankHostedService> _logger;
        private readonly IHostApplicationLifetime _appLifeTime;
        private readonly IMongoDbBankService _service;
        private TerminalState _state = TerminalState.AskForAccountName;
        private string _accountName;

        public MongoDbBankHostedService(
            ILogger<MongoDbBankHostedService> logger,
            IHostApplicationLifetime appLifetime,
            IMongoDbBankService service)
        {
            _logger = logger;
            _appLifeTime = appLifetime;
            _service = service;

            appLifetime.ApplicationStarted.Register(OnStarted);
            appLifetime.ApplicationStopping.Register(OnStopping);
            appLifetime.ApplicationStopped.Register(OnStopped);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogTrace("1. StartAsync has been called.");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogTrace("4. StopAsync has been called.");
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogTrace("2. OnStarted has been called.");

            Task.Run(async () =>
            {
                Thread.Sleep(1000);
                while (true)
                {
                    if (_state == TerminalState.AskForAccountName)
                    {
                        AskForAccountName();
                        continue;
                    }

                    if (_state == TerminalState.ModifyAccountBalance)
                    {
                        await ModifyAccountBalance();
                        continue;
                    }

                    if (_state == TerminalState.ShowTotalBankBalance)
                    {
                        await ShowTotalBankbalance();
                        continue;
                    }

                    if (_state == TerminalState.ExitApplication)
                    {
                        _appLifeTime.StopApplication();
                    }
                }
            });
        }

        private void AskForAccountName()
        {
            Console.WriteLine("Please enter account name: ");
            _accountName = Console.ReadLine();
            _state = (string.IsNullOrEmpty(_accountName)) ? TerminalState.ShowTotalBankBalance : TerminalState.ModifyAccountBalance;
        }

        private async Task ModifyAccountBalance()
        {
            string amountString;
            int amount;

            while (true)
            {
                Console.WriteLine("Enter transaction amount: ");
                amountString = Console.ReadLine();

                if (!int.TryParse(amountString, out amount))
                {
                    Console.WriteLine("Amount must be a number. Please try again.");
                    continue;
                }
                break;
            }

            Console.WriteLine("Updating account balance");
            await _service.ModifyBalance(_accountName, amount);
            Console.WriteLine("New account balance: ");
            double? balance = await _service.GetBalance(_accountName);
            Console.WriteLine(balance);
            _state = TerminalState.AskForAccountName;
        }

        private async Task ShowTotalBankbalance()
        {
            _state = TerminalState.ExitApplication;
            Console.WriteLine("Bank total amount balance: ");
            double? balance = await _service.GetBankTotalBalance();
            Console.WriteLine(balance);
            Console.WriteLine("Press enter to exit the application");
            
            string input = Console.ReadLine();
            _appLifeTime.StopApplication();
        }


        private void OnStopping()
        {
            _logger.LogTrace("3. OnStopping has been called.");
        }

        private void OnStopped()
        {
            _logger.LogTrace("5. OnStopped has been called.");
        }
    }
}
