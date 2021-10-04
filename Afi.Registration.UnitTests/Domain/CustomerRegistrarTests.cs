using System;
using System.Threading.Tasks;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Repositories;
using Afi.Registration.Domain.Services;
using FluentAssertions;
using Telerik.JustMock;
using Xunit;

namespace Afi.Registration.UnitTests.Domain
{
    public class CustomerRegistrarTests
    {
        [Fact]
        public async Task RegisterAsync_NoPolicy_ThrowsException()
        {
            // Arrange
            var stubPolicyRepo = Mock.Create<IPolicyRepository>();
            Mock.Arrange(() => stubPolicyRepo.FindAsync(Arg.AnyString, Arg.AnyBool))
                .Returns(Task.FromResult<Policy>(null));
            var sut = GetSut(policyRepository: stubPolicyRepo);
            var customer = new Customer();
            var policyRef = string.Empty;

            // Act
            Func<Task> act = () => sut.RegisterAsync(customer, policyRef);

            // Assert
            await act.Should()
                .ThrowAsync<PersistenceException>()
                .WithMessage("No matching policy found.");
        }

        [Fact]
        public async Task RegisterAsync_AlreadyRegistered_ThrowsException()
        {
            // Arrange
            var stubCustomerRepo = Mock.Create<ICustomerRepository>();
            Mock.Arrange(() => stubCustomerRepo.FindAsync(Arg.IsAny<Customer>(), Arg.AnyString))
                .Returns(Task.FromResult(new Customer()));
            var sut = GetSut(customerRepository: stubCustomerRepo);
            var customer = new Customer();
            var policyRef = string.Empty;

            // Act
            Func<Task> act = () => sut.RegisterAsync(customer, policyRef);

            // Assert
            await act.Should()
                .ThrowAsync<PersistenceException>()
                .WithMessage("Customer already registered.");
        }

        [Fact]
        public async Task RegisterAsync_HappyPath_CallToPersistData()
        {
            // Arrange
            var mockCustomerRepo = Mock.Create<ICustomerRepository>();
            Mock.Arrange(() => mockCustomerRepo.FindAsync(Arg.IsAny<Customer>(), Arg.AnyString))
                .Returns(Task.FromResult<Customer>(null));
            var sut = GetSut(customerRepository: mockCustomerRepo);
            var customer = new Customer();
            var policyRef = string.Empty;

            // Act
            var result = await sut.RegisterAsync(customer, policyRef);

            // Assert
            Mock.Assert(
                () => mockCustomerRepo.AddToPolicyAsync(customer, policyRef),
                Occurs.Once());
        }

        private static CustomerRegistrar GetSut(
            IItemValidator<Customer> customerValidator = null,
            ICustomerRepository customerRepository = null,
            IPolicyRepository policyRepository = null)
        {
            return new CustomerRegistrar(
                customerValidator ?? Mock.Create<IItemValidator<Customer>>(),
                customerRepository ?? Mock.Create<ICustomerRepository>(),
                policyRepository ?? Mock.Create<IPolicyRepository>());
        }
    }
}
