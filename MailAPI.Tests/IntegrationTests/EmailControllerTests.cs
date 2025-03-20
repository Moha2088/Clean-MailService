using MailAPI.Domain.Entities;
using MailAPI.Infrastructure.Data;
using MailAPI.Tests.IntegrationTests.Common;
using MailAPI.Tests.IntegrationTests.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace MailAPI.Tests.IntegrationTests
{
    public class EmailControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly DataContext _context;

        public EmailControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
            var scope = factory.Services.CreateScope();
            _context = scope.ServiceProvider.GetRequiredService<DataContext>();
        }



        [Fact]
        public async Task GetEmails_ShouldReturnNotFound_WhenNoEmailsExist()
        {
            _context.Users.Add(
                new User
                {
                    Name = "Test",
                   Email = "Test@test.com",
                   Password = "Test123"
                }
            );

            await _context.SaveChangesAsync();

            var result = await _client.GetAsync(BaseAddress.Email);

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async Task GetEmail_ShouldReturnNotFound_WhenEmailDoesntExist()
        {
            var result = await _client.GetAsync($"{BaseAddress.Email}/1");

            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }
    }
}
