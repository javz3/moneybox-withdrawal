using Moneybox.App;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Moneybox.Tests
{
    public class UserModelTest
    {
        [Test]
        public void UserModelValidate_GivenMandatoryFieldsPopulated_ReturnsTrue()
        {
            var userModel = new User
            {
                Id = It.IsAny<Guid>(),
                Name = "Joe",
                Email = "joe.bloggs@apple.com"
            };

            var context = new ValidationContext(userModel, null, null);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(userModel, context, results, true);            

            var fieldsWithErrors = results.Select(x => x.MemberNames?.FirstOrDefault()).ToList();
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Email)));
            Assert.IsTrue(valid);
        }

        [Test]
        public void UserModelValidate_GivenMissingConfirmFields_ReturnsFalse()
        {
            var userModel = new User
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                Email = null
            };

            var context = new ValidationContext(userModel, null, null);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(userModel, context, results, true);

            var fieldsWithErrors = results.Select(x => x.MemberNames?.FirstOrDefault()).ToList();
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Id)));
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Name)));
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Email)));
            Assert.IsTrue(valid);
        }

        [Test]
        public void UserModelValidate_GivenNoMandatoryFieldsPopulated_ReturnsFalse()
        {
            var userModel = new User();

            var context = new ValidationContext(userModel, null, null);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(userModel, context, results);

            var fieldsWithErrors = results.Select(x => x.MemberNames.FirstOrDefault()).ToList();

            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Id)));
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Name)));
            Assert.IsFalse(fieldsWithErrors.Contains(nameof(userModel.Email)));
            Assert.IsTrue(valid);
        }
    }
}