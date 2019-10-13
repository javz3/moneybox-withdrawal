using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;

namespace Moneybox.Tests.Services
{
    [TestFixture]
    public class WithdrawMoneyTest
    {
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IBalanceService> _balanceService;
        private Mock<IWithdrawService> _withdrawService;

        private Account _from;
        private Account _to;

        private WithdrawMoney _sut;

        [SetUp]
        public void _SetUp()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _balanceService = new Mock<IBalanceService>();
            _withdrawService = new Mock<IWithdrawService>();

            _from = AccountBuilder.Build(5000m, 200m, 100m);
            _to = AccountBuilder.Build(500m, 200m, 100m);

            _accountRepository.Setup(x => x.GetAccountById(_from.Id)).Returns(_from);
            _accountRepository.Setup(x => x.GetAccountById(_to.Id)).Returns(_to);

            _sut = new WithdrawMoney(_accountRepository.Object, _balanceService.Object, _withdrawService.Object);
        }

        [TestCase("5")]
        [TestCase("8")]
        public void GivenInsufficientFunds_Execute_NotifyUserAndNotWithdraw(string amount)
        {
            //Arrange
            var startingFromBalance = _from.Balance;
            _balanceService.Setup(x => x.IsInsufficientFunds(_from, decimal.Parse(amount))).Returns(true);

            //Act
            _sut.Execute(_from.Id, decimal.Parse(amount));

            //Assert
            _withdrawService.Verify(x => x.Withdraw(_from, decimal.Parse(amount)), Times.Never());
        }

        [TestCase("5")]
        [TestCase("8")]
        public void GivenSufficientFunds_Execute_Withdraw(string amount)
        {
            //Arrange
            var startingFromBalance = _from.Balance;
            _balanceService.Setup(x => x.IsInsufficientFunds(_from, decimal.Parse(amount))).Returns(false);

            //Act
            _sut.Execute(_from.Id, decimal.Parse(amount));

            //Assert
            _withdrawService.Verify(x => x.Withdraw(_from, decimal.Parse(amount)));
        }
    }
}
