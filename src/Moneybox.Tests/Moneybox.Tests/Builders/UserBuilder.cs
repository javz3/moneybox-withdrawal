using AutoFixture;
using Moneybox.App;

namespace Moneybox.Tests
{
    public static class UserBuilder
    {
        public static User Build(string email, string forename, string surname)
        {
            var fixture = new Fixture();
            var model = fixture.Build<User>()
                .With(x => x.Email, email)
                .With(x => x.Forename, forename)
                .With(x => x.Surname, surname)
                .Create();

            return model;
        }
    }
}