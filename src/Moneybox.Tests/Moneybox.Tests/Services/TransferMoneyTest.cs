﻿using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;

namespace Moneybox.Tests.Services
{
    [TestFixture]
    public class TransferMoneyTest
    {
        private Mock<IAccountRepository> _accountRepository;
        private Mock<IBalanceService> _balanceService;
        private Mock<INotificationService> _notificationService;
        private Mock<IWithdrawService> _withdrawService;

        private Account _from;
        private Account _to;

        private TransferMoney _sut;

        [SetUp]
        public void _SetUp()
        {
            _accountRepository = new Mock<IAccountRepository>();
            _balanceService = new Mock<IBalanceService>();
            _notificationService = new Mock<INotificationService>();
            _withdrawService = new Mock<IWithdrawService>();

            _from = AccountBuilder.Build(5000m, 200m, 100m);
            _to = AccountBuilder.Build(500m, 200m, 100m);

            _accountRepository.Setup(x => x.GetAccountById(_from.Id)).Returns(_from);
            _accountRepository.Setup(x => x.GetAccountById(_to.Id)).Returns(_to);

            _sut = new TransferMoney(_accountRepository.Object, _balanceService.Object, _notificationService.Object, _withdrawService.Object);
        }

        [TestCase("500000")]
        [TestCase("8000000")]
        public void Execute_IsLimitReached_NotifyUser(string amount)
        {
            //Act
            _sut.Execute(_from.Id, _to.Id, decimal.Parse(amount));

            //Assert
            _notificationService.Verify(x => x.NotifyApproachingPayInLimit(_to.User.Email));
        }

        [TestCase("5")]
        [TestCase("8")]
        public void Execute_UpdateAccountTo(string amount)
        {
            //Arrange
            var startingToBalance = _to.Balance;
            _balanceService.Setup(x => x.IsInsufficientFunds(_from, decimal.Parse(amount))).Returns(false);

            //Act
            _sut.Execute(_from.Id, _to.Id, decimal.Parse(amount));

            //Assert
            Assert.That(_to.Balance, Is.EqualTo(startingToBalance + decimal.Parse(amount)));
            _notificationService.Verify(x => x.NotifyApproachingPayInLimit(_to.User.Email), Times.Never());
        }
    }
}
