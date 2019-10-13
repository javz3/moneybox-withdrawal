using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;

namespace Moneybox.App.Features
{
    public class WithdrawService : IWithdrawService
    {
        private readonly IAccountRepository _accountRepository;
        public WithdrawService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Withdraw(Account accountFrom, decimal amount)
        {
            accountFrom.Balance -= amount;
            accountFrom.Withdrawn -= amount;
            _accountRepository.Update(accountFrom);
        }
    }
}