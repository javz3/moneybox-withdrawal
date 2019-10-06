using Moneybox.App.Domain.Services;
using Moq;
using NUnit.Framework;
using System;

namespace Moneybox.Tests.Services
{
    class BalanceServiceTest
    {
        private Mock<IBalanceService> _balanceService;

        [SetUp]
        public void _SetUp()
        {           
            _balanceService = new Mock<IBalanceService>();
        }

        [Test]
        public void CheckIfIsInsufficientFundsThrowsException()
        {
            _balanceService.Setup(s => s.IsInsufficientFunds(null, decimal.Zero)).Throws(new InvalidOperationException());
        }

        [Test]
        public void CheckIfIsLowBalanceIsVerifiable()
        {
            _balanceService.Setup(s => s.IsLowBalance(null, decimal.Zero)).Verifiable();
        }
    }
}
