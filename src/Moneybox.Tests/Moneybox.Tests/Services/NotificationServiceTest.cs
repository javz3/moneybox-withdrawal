using Moneybox.App;
using Moneybox.App.Domain.Services;
using Moq;
using NUnit.Framework;
using System;

namespace Moneybox.Tests.Services
{
    class NotificationServiceTest
    {
        private Mock<INotificationService> _notificationService;

        [SetUp]
        public void _SetUp()
        {   
            _notificationService = new Mock<INotificationService>();       
        }

        [Test]
        public void GetNotification_GivenAccountIsNotNull()
        {
            var accountModel = new Account
            {
                Id = It.IsAny<Guid>(),
                User = new User
                {
                    Id = It.IsAny<Guid>(),
                    Name = "Joe",
                    Email = "joe.bloggs@apple.com"
                },
                Balance = 500m,
                Withdrawn = 200m,
                PaidIn = 100m
            };

            _notificationService.Setup(s => s.NotifyFundsLow(accountModel.User.Email)).Verifiable();
        }
    }
}
