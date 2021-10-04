using System;
using System.Linq;
using System.Threading.Tasks;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Persistence.Repositories;
using FluentAssertions;
using Xunit;

namespace Afi.Registration.UnitTests.Persistence
{
    public class CustomerRepositoryTests
    {
        [Fact]
        public async Task AddToPolicyAsync_NoPolicy_ThrowsException()
        {
            // Arrange
            var memDb = Utils.SeedInMemDb();
            var sut = new CustomerRepository(memDb);

            // Act
            Func<Task> act = () => sut.AddToPolicyAsync(new(), string.Empty);

            // Assert
            await act.Should()
                .ThrowAsync<PersistenceException>()
                .WithMessage("Policy not found");
        }

        [Fact]
        public async Task AddToPolicyAsync_ValidPolicy_ReturnsId()
        {
            // Arrange
            var customer = new Customer { Forename = "Bob", Surname = "Smith" };
            var policy = new Policy { PolicyReference = "REF" };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new CustomerRepository(memDb);

            // Act
            var result = await sut.AddToPolicyAsync(customer, policy.PolicyReference);

            // Assert
            result.Should().NotBe(default);
        }

        [Fact]
        public async Task AddToPolicyAsync_UnactivatedPolicy_IsActivated()
        {
            // Arrange
            var customer = new Customer { Forename = "Bob", Surname = "Smith" };
            var policy = new Policy { PolicyReference = "REF" };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new CustomerRepository(memDb);

            // Act
            var result = await sut.AddToPolicyAsync(customer, policy.PolicyReference);

            // Assert
            memDb.Policies.Single(r => r.PolicyReference == policy.PolicyReference)
                .ActivatedOn.Should().NotBe(default);
        }

        [Fact]
        public async Task AddToPolicyAsync_ActivatedPolicy_IsNotReActivated()
        {
            // Arrange
            var activationDate = new DateTime(2015, 10, 26);
            var customer = new Customer { Forename = "Bob", Surname = "Smith" };
            var policy = new Policy { PolicyReference = "REF", ActivatedOn = activationDate };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new CustomerRepository(memDb);

            // Act
            var result = await sut.AddToPolicyAsync(customer, policy.PolicyReference);

            // Assert
            memDb.Policies.Single(r => r.PolicyReference == policy.PolicyReference)
                .ActivatedOn.Should().Be(activationDate);
        }

        [Fact]
        public async Task FindAsync_DoesNotExist_ReturnsNull()
        {
            // Arrange
            var memDb = Utils.SeedInMemDb();
            var sut = new CustomerRepository(memDb);

            // Act
            var result = await sut.FindAsync(new(), string.Empty);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task FindAsync_DoesExist_ReturnsNotNull()
        {
            // Arrange
            var customer = new Customer { Forename = "Bob", Surname = "Smith" };
            var policy = new Policy { PolicyReference = "REF", Owner = customer };
            var memDb = Utils.SeedInMemDb(db =>
            {
                db.Policies.Add(policy);
            });
            var sut = new CustomerRepository(memDb);
            var query = new Customer
            {
                Forename = customer.Forename,
                Surname = customer.Surname,
            };

            // Act
            var result = await sut.FindAsync(query, policy.PolicyReference);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
