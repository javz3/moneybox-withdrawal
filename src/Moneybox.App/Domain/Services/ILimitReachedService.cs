namespace Moneybox.App.Domain.Services
{
    public interface ILimitReachedService
    {
        void LimitReached(decimal limit, decimal amount, string errorMessage);
    }
}