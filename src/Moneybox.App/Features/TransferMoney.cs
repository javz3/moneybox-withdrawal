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
        private readonly IWithdrawService _withdrawService;

        public TransferMoney(IAccountRepository accountRepository, IBalanceService balanceService, INotificationService notificationService, IWithdrawService withdrawService)
        {
            _accountRepository = accountRepository;
            _balanceService = balanceService;
            _notificationService = notificationService;
            _withdrawService = withdrawService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);
            var accountTo = _accountRepository.GetAccountById(toAccountId);            

            if (_balanceService.IsInsufficientFunds(accountFrom, amount) || _balanceService.IsLowBalance(accountFrom, amount))
            {                
                return; 
            }

            IsLimitReached(accountTo, amount);
            _withdrawService.Withdraw(accountFrom, amount);
            UpdateAccountTo(amount, accountTo);
        }

        private void IsLimitReached(Account accountTo, decimal amount)
        {
            if (accountTo.PaidIn + amount > Account.PayInLimit)
            {
                _notificationService.NotifyApproachingPayInLimit(accountTo.User.Email);
                return;
            }
        }

        private void UpdateAccountTo(decimal amount, Account accountTo)
        {
            accountTo.Balance += amount;
            accountTo.PaidIn += amount;
            _accountRepository.Update(accountTo);
        }
    }
}