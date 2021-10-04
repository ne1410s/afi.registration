using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Afi.Registration.Api.Models;
using Afi.Registration.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using RefactorThis.IntegrationTests;
using Xunit;

namespace Afi.Registration.IntegrationTests.Endpoints
{
    public class RegistrationEndpointTests : IDisposable
    {
        private readonly HttpClient client;

        public RegistrationEndpointTests()
        {
            var policy = new Policy { PolicyReference = "AA-000000" };
            var appFactory = new IntegrationTestingWebAppFactory("Data Source=test.db", db =>
            {
                db.Policies.Add(policy);
            });
            client = appFactory.CreateClient();
        }

        [Fact]
        public async Task CallEndpoint_HappyPath_ReturnsId()
        {
            var requestPayload = new CustomerRegistrationRequest
            {
                Forename = "Bob",
                Surname = "Smith",
                Email = "test@domain.co",
                PolicyReference = "AA-000000",
                DateOfBirth = new DateTime(2000, 10, 26),
            };
            var requestJson = JsonConvert.SerializeObject(requestPayload);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/registration", requestContent);
            var responseJson = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
            var result = JsonConvert.DeserializeObject<SuccessfulRegistrationResponse>(responseJson);

            result?.CustomerId.Should().NotBe(default);
        }

        [Fact]
        public async Task CallEndpoint_InvalidDataState_Returns400()
        {
            var requestPayload = new CustomerRegistrationRequest
            {
                Forename = "Bob",
                Surname = "Smith",
                Email = "test@domain.co",
                PolicyReference = "AA-000001",
                DateOfBirth = new DateTime(2000, 10, 26),
            };
            var requestJson = JsonConvert.SerializeObject(requestPayload);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/registration", requestContent);

            ((int)httpResponse.StatusCode).Should().Be(400);
        }

        [Fact]
        public async Task CallEndpoint_InvalidRequestData_Returns422()
        {
            var requestPayload = new CustomerRegistrationRequest
            {
                Forename = "Bob",
                Surname = "Smith",
                Email = "test@domain.co",
                PolicyReference = "AA-000000",
                DateOfBirth = DateTime.Today.AddYears(-16),
            };
            var requestJson = JsonConvert.SerializeObject(requestPayload);
            var requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            var httpResponse = await client.PostAsync("api/registration", requestContent);

            ((int)httpResponse.StatusCode).Should().Be(422);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            client?.Dispose();
        }
    }
}
