using Moneybox.App;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moneybox.Tests
{
    public class AccountModelTest
    {
        [Test]
        public void AccountModelValidate_GivenMandatoryFieldsPopulated_ReturnsTrue()
        {
            // Arrange
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

            var context = new ValidationContext(accountModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(accountModel, context, results, true);            

            // Assert
            var fieldsWithErrors = results.Select(x => x.MemberNames?.FirstOrDefault()).ToList();
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(accountModel.User.Email)));
            Assert.IsTrue(valid);
        }

        [Test]
        public void AccountModelValidate_GivenMissingConfirmFields_ReturnsFalse()
        {
            // Arrange
            var accountModel = new Account
            {
                Id = Guid.Empty,
                User = null,
                Balance = 500m,
                Withdrawn = 200m,
                PaidIn = 0
            };

            var context = new ValidationContext(accountModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(accountModel, context, results, true);

            // Assert
            var fieldsWithErrors = results.Select(x => x.MemberNames?.FirstOrDefault()).ToList();
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(accountModel.Id)));
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(accountModel.User.Email)));
            Assert.IsTrue(valid);
        }

        [Test]
        public void AccountModelValidate_GivenNoMandatoryFieldsPopulated_ReturnsFalse()
        {
            // Arrange
            var accountModel = new Account();

            var context = new ValidationContext(accountModel, null, null);
            var results = new List<ValidationResult>();

            // Act
            var valid = Validator.TryValidateObject(accountModel, context, results);

            // Assert
            var fieldsWithErrors = results.Select(x => x.MemberNames.FirstOrDefault()).ToList();

            Assert.IsFalse(fieldsWithErrors.Contains(nameof(accountModel.User)));
            Assert.IsTrue(valid);
        }
    }
}