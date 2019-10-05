using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceService _balanceService;

        public WithdrawMoney(IAccountRepository accountRepository, IBalanceService balanceService)
        {
            _accountRepository = accountRepository;
            _balanceService = balanceService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);

            _balanceService.IsInsufficientFunds(accountFrom, amount);
            _balanceService.IsLowBalance(accountFrom, amount);
            _balanceService.IsLimitReached(Account.WithdrawalLimit, amount, "Account withdrawal limit reached");

            accountFrom.Withdrawn = accountFrom.Withdrawn - amount;

            _accountRepository.Update(accountFrom);
        }
    }
}