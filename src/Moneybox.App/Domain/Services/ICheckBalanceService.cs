namespace Moneybox.App.Domain.Services
{
    public interface ICheckBalanceService
    {
        void CheckingBalance(Account account, decimal fromBalance);
    }
}