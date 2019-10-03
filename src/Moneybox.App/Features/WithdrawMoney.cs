using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICheckBalanceService _checkBalanceService;
        private readonly ILimitReachedService _limitReached;

        public WithdrawMoney(IAccountRepository accountRepository, ICheckBalanceService checkbalanceService, ILimitReachedService limitReached)
        {
            _accountRepository = accountRepository;
            _checkBalanceService = checkbalanceService;
            _limitReached = limitReached;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);

            _checkBalanceService.CheckingBalance(accountFrom, amount);
            _limitReached.LimitReached(Account.WithdrawalLimit, amount, "Account withdrawal limit reached");

            accountFrom.Withdrawn = accountFrom.Withdrawn - amount;

            _accountRepository.Update(accountFrom);
        }
    }
}