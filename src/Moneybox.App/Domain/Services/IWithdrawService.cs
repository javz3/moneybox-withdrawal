namespace Moneybox.App.Domain.Services
{
    public interface IWithdrawService
    {
        void Withdraw(Account accountFrom, decimal amount);
    }
}