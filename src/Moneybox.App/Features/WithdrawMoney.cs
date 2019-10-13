using System;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IBalanceService _balanceService;
        private readonly IWithdrawService _withdrawService;

        public WithdrawMoney(IAccountRepository accountRepository, IBalanceService balanceService, IWithdrawService withdrawService)
        {
            _accountRepository = accountRepository;
            _balanceService = balanceService;
            _withdrawService = withdrawService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var accountFrom = _accountRepository.GetAccountById(fromAccountId);

            if (_balanceService.IsInsufficientFunds(accountFrom, amount) || _balanceService.IsLowBalance(accountFrom, amount))
            {
                return;
            }

            _withdrawService.Withdraw(accountFrom, amount);
        }
    }
}