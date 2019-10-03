using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class InsufficientFundsCheck : IBalanceCheckerService
    {
        public void Check(Account account, decimal amount)
        {
            if (account.Balance - amount < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }
        }
    }
}