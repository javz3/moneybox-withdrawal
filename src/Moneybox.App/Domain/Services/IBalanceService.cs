namespace Moneybox.App.Domain.Services
{
    public interface IBalanceService
    {
        bool IsInsufficientFunds(Account account, decimal amount);

        bool IsLowBalance(Account account, decimal amount);

        bool IsLimitReached(decimal limit, decimal amount);
    }
}