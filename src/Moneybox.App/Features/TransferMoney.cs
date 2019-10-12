using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceService _balanceService;
        private readonly INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, IBalanceService balanceService, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _balanceService = balanceService;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);
            var accountTo = _accountRepository.GetAccountById(toAccountId);            

            if (_balanceService.IsInsufficientFunds(accountFrom, amount) || _balanceService.IsLowBalance(accountFrom, amount))
            {
                _notificationService.NotifyApproachingPayInLimit(accountTo.User.Email);
                return; 
            }

            var paidIn = accountTo.PaidIn + amount;

            if (_balanceService.IsLimitReached(Account.PayInLimit, paidIn))
            {
                _notificationService.NotifyFundsLow(accountTo.User.Email);
                return;
            }

            UpdateAccountFrom(amount, accountFrom);
            UpdateAccountTo(amount, accountTo);
        }

        private void UpdateAccountFrom(decimal amount, Account accountFrom)
        {
            accountFrom.Balance -= amount;
            accountFrom.Withdrawn -= amount;
            _accountRepository.Update(accountFrom);
        }

        private void UpdateAccountTo(decimal amount, Account accountTo)
        {
            accountTo.Balance += amount;
            accountTo.PaidIn += amount;
            _accountRepository.Update(accountTo);
        }
    }
}