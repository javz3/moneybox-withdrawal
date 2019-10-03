using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICheckBalanceService _checkBalanceService;
        private readonly ILimitReachedService _limitReachedService;
        private readonly INotificationService _notificationService;

        public TransferMoney(IAccountRepository accountRepository, ICheckBalanceService checkbalanceService, ILimitReachedService limitReached, INotificationService notificationService)
        {
            _accountRepository = accountRepository;
            _checkBalanceService = checkbalanceService;
            _limitReachedService = limitReached;
            _notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);
            var accountTo = _accountRepository.GetAccountById(toAccountId);

            _checkBalanceService.CheckingBalance(accountFrom, amount);

            var paidIn = accountTo.PaidIn + amount;

            ApproachingPayInLimit(accountTo, paidIn);
            _limitReachedService.LimitReached(Account.PayInLimit, paidIn, "Account pay in limit reached");

            accountFrom.Balance = accountFrom.Balance - amount;
            accountFrom.Withdrawn = accountFrom.Withdrawn - amount;

            accountTo.Balance = accountTo.Balance + amount;
            accountTo.PaidIn = accountTo.PaidIn + amount;

            _accountRepository.Update(accountFrom);
            _accountRepository.Update(accountTo);
        }

        private void ApproachingPayInLimit(Account to, decimal paidIn)
        {
            if (Account.PayInLimit - paidIn < 500m)
            {
                _notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
        }
    }
}