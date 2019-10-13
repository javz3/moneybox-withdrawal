namespace Moneybox.App.Domain.Services
{
    public interface IBalanceService
    {
        bool IsInsufficientFunds(Account accountFrom, decimal amount);

        void IsLowBalance(Account accountFrom, decimal amount);
    }
}