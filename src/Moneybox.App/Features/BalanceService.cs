using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class BalanceService : IBalanceService
    {
        private readonly INotificationService _notificationService;

        public BalanceService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public bool IsInsufficientFunds(Account accountFrom, decimal amount)
        {
            if (accountFrom.Balance - amount < Account.InsufficientFundsLimit)
            {
                _notificationService.NotifyFundsLow(accountFrom.User.Email);
                return true;
            }
            return false;
        }

        public void IsLowBalance(Account accountFrom, decimal amount)
        {
            if (accountFrom.Balance - amount < Account.LowFundsLimit)
            {
                _notificationService.NotifyFundsLow(accountFrom.User.Email);
            }
        }
    }
}