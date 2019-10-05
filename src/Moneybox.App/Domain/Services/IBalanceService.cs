namespace Moneybox.App.Domain.Services
{
    public interface IBalanceService
    {
        void IsInsufficientFunds(Account account, decimal amount);
        void IsLowBalance(Account account, decimal amount);
        void IsLimitReached(decimal limit, decimal amount, string errorMessage);
    }
}