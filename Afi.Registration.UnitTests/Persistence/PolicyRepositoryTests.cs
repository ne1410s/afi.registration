using System;
using System.Threading.Tasks;
using Afi.Registration.Domain.Models;
using Afi.Registration.Persistence.Repositories;
using FluentAssertions;
using Xunit;

namespace Afi.Registration.UnitTests.Persistence
{
    public class PolicyRepositoryTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task FindAsync_ExistingPolicy_IsReturned(bool recordIsActivated)
        {
            // Arrange
            DateTime? activatedOn = recordIsActivated ? new DateTime(2015, 10, 26) : null;
            var policy = new Policy { PolicyReference = "REF", ActivatedOn = activatedOn };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new PolicyRepository(memDb);

            // Act
            var result = await sut.FindAsync(policy.PolicyReference, null);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task FindAsync_BadActivatedFlag_ReturnsNull(bool recordIsActivated)
        {
            // Arrange
            DateTime? activatedOn = recordIsActivated ? new DateTime(2015, 10, 26) : null;
            var policy = new Policy { PolicyReference = "REF", ActivatedOn = activatedOn };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new PolicyRepository(memDb);

            // Act
            var result = await sut.FindAsync(policy.PolicyReference, !recordIsActivated);

            // Assert
            result.Should().BeNull();
        }
    }
}
