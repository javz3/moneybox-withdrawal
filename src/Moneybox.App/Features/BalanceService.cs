using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class BalanceService : IBalanceService
    {
        private readonly INotificationService _notificationService;

        public BalanceService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void IsInsufficientFunds(Account account, decimal amount)
        {
            if (account.Balance - amount < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }
        }

        public void IsLowBalance(Account account, decimal amount)
        {
            if (account.Balance - amount < 500m)
            {
                _notificationService.NotifyFundsLow(account.User.Email);
            }
        }

        public void IsLimitReached(decimal limit, decimal amount, string errorMessage)
        {
            if (amount > Account.PayInLimit)
            {
                throw new InvalidOperationException(errorMessage);
            }
        }
    }
}