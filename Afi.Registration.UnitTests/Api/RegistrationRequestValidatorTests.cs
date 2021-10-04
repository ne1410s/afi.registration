using System;
using System.Linq;
using Afi.Registration.Api.Models;
using Afi.Registration.Api.Services;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Afi.Registration.UnitTests.Api
{
    public class RegistrationRequestValidatorTests
    {
        private const string ValidEmail = "test@domain.co";
        private const string ValidForename = "Bob";
        private const string ValidSurname = "Smith";
        private const string ValidPolicyRef = "AA-000000";
        private static readonly DateTime ValidDateOfBirth = DateTime.Today.AddYears(-21);

        [Fact]
        public void ValidateItem_Valid_DoesNotThrow()
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
                PolicyReference = ValidPolicyRef,
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should().NotThrow<ValidationException>();
        }

        [Theory]
        [InlineData("test")]
        [InlineData("test@@domain.co")]
        [InlineData("test.domain.co")]
        public void ValidateItem_EmailInvalid_ThrowsException(string invalidEmail)
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = invalidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
                PolicyReference = ValidPolicyRef,
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("not a valid email"));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(51)]
        public void ValidateItem_ForenameLengthInvalid_ThrowsException(int forenameLength)
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = ValidEmail,
                Forename = new string('B', forenameLength),
                Surname = ValidSurname,
                PolicyReference = ValidPolicyRef,
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("'Forename' must be between 3 and 50 characters"));
        }

        [Theory]
        [InlineData(2)]
        [InlineData(51)]
        public void ValidateItem_SurnameLengthInvalid_ThrowsException(int surnameLength)
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = new string('S', surnameLength),
                PolicyReference = ValidPolicyRef,
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("'Surname' must be between 3 and 50 characters"));
        }

        [Theory]
        [InlineData("000000")]
        [InlineData("A-000000")]
        [InlineData("AAA-000000")]
        [InlineData("AA-00000")]
        [InlineData("AA-0000000")]
        [InlineData("AA000000")]
        public void ValidateItem_PolicyRefInvalid_ThrowsException(string invalidPolicyRef)
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
                PolicyReference = invalidPolicyRef,
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("'Policy Reference' is not in the correct format."));
        }

        [Fact]
        public void ValidateItem_DateOfBirthInvalid_ThrowsException()
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
                PolicyReference = ValidPolicyRef,
                DateOfBirth = DateTime.Today.AddYears(-18).AddDays(1),
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("Customer must be 18 or over."));
        }

        [Theory]
        [InlineData(true, true, true)]
        [InlineData(false, true, true)]
        [InlineData(true, false, true)]
        [InlineData(false, false, false)]
        public void ValidateItem_EmailOrDateOfBirth_IsRequired(
            bool hasEmail,
            bool hasDateOfBirth,
            bool expectedValid)
        {
            // Arrange
            IItemValidator<CustomerRegistrationRequest> sut = new RegistrationRequestValidator();
            var item = new CustomerRegistrationRequest
            {
                Email = hasEmail ? ValidEmail : null,
                Forename = ValidForename,
                Surname = ValidSurname,
                PolicyReference = ValidPolicyRef,
                DateOfBirth = hasDateOfBirth ? ValidDateOfBirth : null,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            if (expectedValid)
            {
                act.Should().NotThrow<ValidationException>();
            }
            else
            {
                act.Should().Throw<ValidationException>()
                    .Where(ex => ex.Errors.Any(msg => msg == "'Email' or 'Date of Birth' are required."));
            }
        }
    }
}
