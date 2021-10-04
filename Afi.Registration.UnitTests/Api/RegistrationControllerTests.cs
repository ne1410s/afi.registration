using System.Threading.Tasks;
using Afi.Registration.Api.Controllers;
using Afi.Registration.Api.Models;
using Afi.Registration.Api.Services;
using Afi.Registration.Domain.Errors;
using Afi.Registration.Domain.Models;
using Afi.Registration.Domain.Services;
using Telerik.JustMock;
using Xunit;

namespace Afi.Registration.UnitTests.Api
{
    public class RegistrationControllerTests
    {
        [Fact]
        public async Task RegisterAsync_InvalidRequest_DoesNotCallRegister()
        {
            // Arrange
            var mockRegistrar = Mock.Create<ICustomerRegistrar>();
            var stubValidator = Mock.Create<IItemValidator<CustomerRegistrationRequest>>();            
            var request = new CustomerRegistrationRequest();
            Mock.Arrange(() => stubValidator.ValidateItem(request))
                .Throws<ValidationException>();   
            var sut = GetSut(stubValidator, registrar: mockRegistrar);

            // Act
            try
            {
                _ = await sut.RegisterAsync(request);
            }
            catch (ValidationException)
            { /**/ }
            finally
            {
                // Assert
                Mock.Assert(
                    () => mockRegistrar.RegisterAsync(Arg.IsAny<Customer>(), Arg.AnyString),
                    Occurs.Never());
            }
        }

        [Fact]
        public async Task RegisterAsync_ValidRequest_CallsRegister()
        {
            // Arrange
            var mockRegistrar = Mock.Create<ICustomerRegistrar>();
            var sut = GetSut(registrar: mockRegistrar);
            var request = new CustomerRegistrationRequest();

            // Act
            var result = await sut.RegisterAsync(request);

            // Assert
            Mock.Assert(
                () => mockRegistrar.RegisterAsync(Arg.IsAny<Customer>(), Arg.AnyString),
                Occurs.Once());
        }

        private static RegistrationController GetSut(
            IItemValidator<CustomerRegistrationRequest> requestValidator = null,
            ICustomerRequestMapper requestMapper = null,
            ICustomerRegistrar registrar = null)
        {
            return new RegistrationController(
                requestValidator ?? Mock.Create<IItemValidator<CustomerRegistrationRequest>>(),
                requestMapper ?? Mock.Create<ICustomerRequestMapper>(),
                registrar ?? Mock.Create<ICustomerRegistrar>());
        }
    }
}
