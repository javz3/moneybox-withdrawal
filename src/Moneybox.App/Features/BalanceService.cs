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

        public bool IsInsufficientFunds(Account account, decimal amount)
        {
            if (account.Balance - amount < Account.InsufficientFundsLimit)
            {
                return true;
            }
            return false;
        }

        public bool IsLowBalance(Account account, decimal amount)
        {
            if (account.Balance - amount < Account.LowFundsLimit)
            {
                _notificationService.NotifyFundsLow(account.User.Email);
                return true;                
            }
            return false;
        }

        public bool IsLimitReached(decimal limit, decimal amount)
        {
            if (amount > Account.PayInLimit)
            {
                return true;
            }
            return false;
        }
    }
}