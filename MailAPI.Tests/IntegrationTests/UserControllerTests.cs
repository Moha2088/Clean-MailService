using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Infrastructure.Data;
using MailAPI.Tests.IntegrationTests.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text.Json;

namespace MailAPI.Tests.IntegrationTests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public UserControllerTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }


        public static IEnumerable<object[]> UserDtoData => new List<object[]>
        {
            new object[] { "Søren", "søren34@gmail.com", "Søren1234__" },
            new object[] { "Michael", "mich12@gmail.com", "Mike123-" },
            new object[] { "Anders", "anders15@gmail.com", "Anders1212$$" }
        };


        [Theory]
        [MemberData(nameof(UserDtoData))]
        public async Task CreateUser_ShouldReturnCreated_WhenDtoIsValid(string name, string email, string password)
        {
            var userDto = new UserCreateDto(name, email, password);
            var result = await _client.PostAsJsonAsync("api/users", userDto);

            result.EnsureSuccessStatusCode();
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnInternalServerError_WhenValidationFails()
        {
            var userDto = new UserCreateDto("Hans", "h@h.com", "123");
            var result = await _client.PostAsJsonAsync("api/users", userDto);
            
            Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnNotFound_WhenNoUsersExist()
        {
            var results = await _client.GetAsync("api/users");

            Assert.NotNull(results);
            Assert.Equal(HttpStatusCode.NotFound, results.StatusCode);
        }
    }
}