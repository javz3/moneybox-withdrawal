using Moneybox.App;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;

namespace Moneybox.Tests.Services
{
    class BalanceServiceTest
    {
        private Mock<INotificationService> _notificationService;
        private Account _from;
        private BalanceService _sut;

        [SetUp]
        public void _SetUp()
        {           
            _notificationService = new Mock<INotificationService>();
            _from = AccountBuilder.Build(5000m, 200m, 100m);

            _sut = new BalanceService(_notificationService.Object);
        }

        [TestCase("500000")]
        [TestCase("8000000")]
        public void GivenInsufficientFunds_IsInsufficientFunds__NotifyUser(string amount)
        {
            //Act
            var result =_sut.IsInsufficientFunds(_from, decimal.Parse(amount));

            //Assert
            Assert.IsTrue(result);
            _notificationService.Verify(x => x.NotifyFundsLow(_from.User.Email));
        }

        [TestCase("5")]
        public void GivenInsufficientFunds_IsInsufficientFunds_DoesNotNotifyUser(string amount)
        {
            //Act
            _sut.IsInsufficientFunds(_from, decimal.Parse(amount));

            //Assert
            _notificationService.Verify(x => x.NotifyFundsLow(_from.User.Email), Times.Never());
        }

        [TestCase("500000")]
        [TestCase("8000000")]
        public void GivenLowBalance_LowBalance__NotifyUserAndReturnsTrue(string amount)
        {
            //Act
            _sut.IsInsufficientFunds(_from, decimal.Parse(amount));

            //Assert
            _notificationService.Verify(x => x.NotifyFundsLow(_from.User.Email));
        }

        [TestCase("5")]
        public void GivenLowBalance_LowBalance_ReturnsFalse(string amount)
        {
            //Act
            var result = _sut.IsInsufficientFunds(_from, decimal.Parse(amount));

            //Assert
            Assert.IsFalse(result);
        }
    }
}
