using MailAPI.Tests.IntegrationTests.Common;
using MailAPI.Tests.IntegrationTests.Factory;
using System.Net;

namespace MailAPI.Tests.IntegrationTests
{
    public class EmailControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public EmailControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        

        [Fact]
        public async Task GetEmails_ShouldReturnNotFound_WhenNoEmailsExist()
        {
            var result = await _client.GetAsync(BaseAddress.Email);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
