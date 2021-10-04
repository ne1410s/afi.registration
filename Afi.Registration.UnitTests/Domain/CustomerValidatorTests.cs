using System;
using System.Linq;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Services;
using FluentAssertions;
using Xunit;

namespace Afi.Registration.UnitTests.Domain
{
    public class CustomerValidatorTests
    {
        private const string ValidEmail = "test@domain.co";
        private const string ValidForename = "Bob";
        private const string ValidSurname = "Smith";
        private static readonly DateTime ValidDateOfBirth = DateTime.Today.AddYears(-21);

        [Fact]
        public void ValidateItem_Valid_DoesNotThrow()
        {
            // Arrange
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
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
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = invalidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
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
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = ValidEmail,
                Forename = new string('B', forenameLength),
                Surname = ValidSurname,
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
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = new string('S', surnameLength),
                DateOfBirth = ValidDateOfBirth,
            };

            // Act
            Action act = () => sut.ValidateItem(item);

            // Assert
            act.Should()
                .Throw<ValidationException>()
                .Where(ex => ex.Errors.Single().Contains("'Surname' must be between 3 and 50 characters"));
        }

        [Fact]
        public void ValidateItem_DateOfBirthInvalid_ThrowsException()
        {
            // Arrange
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = ValidEmail,
                Forename = ValidForename,
                Surname = ValidSurname,
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
            IItemValidator<Customer> sut = new CustomerValidator();
            var item = new Customer
            {
                Email = hasEmail ? ValidEmail : null,
                Forename = ValidForename,
                Surname = ValidSurname,
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
