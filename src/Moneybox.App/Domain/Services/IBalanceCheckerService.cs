namespace Moneybox.App.Domain.Services
{
    public interface IBalanceCheckerService
    {
        void Check(Account account, decimal amount);
    }
}