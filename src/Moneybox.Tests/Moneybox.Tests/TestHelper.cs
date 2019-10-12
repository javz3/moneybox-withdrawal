using AutoFixture;
using Moneybox.App;

namespace Moneybox.Tests
{
    public static class TestHelper
    {
        public static Account CreateAccount(decimal balance, decimal withdrawn, decimal paidIn)
        {
            var fixture = new Fixture();
            var model = fixture.Build<Account>()
                .With(x => x.Balance, balance)
                .With(x => x.Withdrawn, withdrawn)
                .With(x => x.PaidIn, paidIn)
                .Create();

            return model;
        }
    }
}
