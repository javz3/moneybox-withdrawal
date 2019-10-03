using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class LowBalanceCheck : IBalanceCheckerService
    {
        private readonly INotificationService _notificationService;
        public LowBalanceCheck(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public void Check(Account account, decimal amount)
        {
            if (account.Balance - amount < 500m)
            {
                _notificationService.NotifyFundsLow(account.User.Email);
            }
        }
    }
}