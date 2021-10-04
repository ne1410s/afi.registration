using System;
using Afi.Registration.Api.Models;
using Afi.Registration.Api.Services;
using Xunit;

namespace Afi.Registration.UnitTests.Api
{
    public class CustomerRequestMappingTests
    {
        [Theory]
        [InlineData("Bob", "Smith", "test@domain.co", 1)]
        [InlineData(null, null, null, null)]
        public void Map_OriginalValues_ShouldPropagate(
            string forenameVariant,
            string surnameVariant,
            string emailVariant,
            int? dateOfBirthDaysAgo)
        {
            // Arrange
            var sut = new CustomerRequestMapper();
            var request = new CustomerRegistrationRequest
            {
                Forename = forenameVariant,
                Surname = surnameVariant,
                Email = emailVariant,
                DateOfBirth = dateOfBirthDaysAgo.HasValue
                    ? DateTime.Today.AddDays(-dateOfBirthDaysAgo.Value)
                    : null,
            };

            // Act
            var result = sut.Map(request);

            // Assert
            Assert.Equal(request.Forename, result.Forename);
            Assert.Equal(request.Surname, result.Surname);
            Assert.Equal(request.Email, result.Email);
            Assert.Equal(request.DateOfBirth, result.DateOfBirth);
        }
    }
}
