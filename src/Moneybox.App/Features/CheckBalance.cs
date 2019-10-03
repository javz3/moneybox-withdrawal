using Moneybox.App.Domain.Services;
using System.Collections.Generic;

namespace Moneybox.App.Features
{
    public class CheckBalance : ICheckBalanceService
    {
        private readonly IEnumerable<IBalanceCheckerService> _balanceCheckers;
        public CheckBalance(IEnumerable<IBalanceCheckerService> balanceCheckers)
        {
            _balanceCheckers = balanceCheckers;
        }

        public void CheckingBalance(Account account, decimal amount)
        {
            foreach (var balanceChecker in _balanceCheckers)
            {
                balanceChecker.Check(account, amount);
            }
        }
    }
}