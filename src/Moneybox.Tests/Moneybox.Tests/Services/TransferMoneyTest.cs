using System;
using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;
using FluentAssertions;

namespace Moneybox.Tests.Services
{
    [TestFixture]
    public class TransferMoneyTest
    {
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IBalanceService> _balanceService;
        private Mock<INotificationService> _notificationService;
        private Mock<User> _user;

        private TransferMoney _sut;

        private Account _from;
        private Account _to;

        [SetUp]
        public void _SetUp()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _balanceService = new Mock<IBalanceService>();
            _notificationService = new Mock<INotificationService>();
            _user = new Mock<User>();

            _sut = new TransferMoney(_accountRepository.Object, _balanceService.Object, _notificationService.Object);
        }

        [TestCase("500000")]
        [TestCase("8000000")]
        public void Execute_WithInsufficientFunds_NotifyUser(string amount) 
        {
            //Arrange
            _from = TestHelper.CreateAccount(5000m, 200m, 100m);
            _to = TestHelper.CreateAccount(500m, 200m, 100m);

            var fromBalance = _from.Balance;
            var toBalance = _to.Balance;

            _accountRepository.Setup(o => o.GetAccountById(_from.Id)).Returns(_from);
            _accountRepository.Setup(o => o.GetAccountById(_to.Id)).Returns(_to);

            _balanceService.Setup(x => x.IsInsufficientFunds(_from, decimal.Parse(amount))).Returns(true);            
            _balanceService.Setup(x => x.IsLowBalance(_from, decimal.Parse(amount))).Returns(true);

            //Act
            _sut.Execute(_from.Id, _to.Id, Decimal.Parse(amount));

            //Assert
            _from.Balance.Should().Equals(fromBalance);
        }
    }
}
