using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Features;
using Moq;
using NUnit.Framework;

namespace Moneybox.Tests.Services
{
    [TestFixture]
    public class WithdrawServiceTest
    {
        private Mock<IAccountRepository> _accountRepository;

        private Account _from;

        private WithdrawService _sut;

        [SetUp]
        public void _SetUp()
        {
            _accountRepository = new Mock<IAccountRepository>();

            _from = AccountBuilder.Build(5000m, 200m, 100m);

            _accountRepository.Setup(x => x.GetAccountById(_from.Id)).Returns(_from);

            _sut = new WithdrawService(_accountRepository.Object);
        }

        [TestCase("5")]
        [TestCase("8")]
        public void Execute_UpdateAccountFrom(string amount)
        {
            //Arrange
            var startingFromBalance = _from.Balance;            

            //Act
            _sut.Withdraw(_from, decimal.Parse(amount));

            //Assert
            Assert.That(_from.Balance, Is.EqualTo(startingFromBalance - decimal.Parse(amount)));
        }
    }
}
