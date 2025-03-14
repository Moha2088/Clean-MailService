using MailAPI.Application.Commands.Users;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Tests.IntegrationTests.Common;
using MailAPI.Tests.IntegrationTests.Factory;
using System.Net;
using System.Net.Http.Json;

namespace MailAPI.Tests.IntegrationTests
{
    public class UserControllerTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;

        public UserControllerTests(CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
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
            var userDto = new UserCreateCommand(name, email, password);
            var result = await _client.PostAsJsonAsync(BaseAddress.User, userDto);

            result.EnsureSuccessStatusCode();
            Assert.NotNull(result);
            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ShouldReturnBadRequest_WhenValidationFails()
        {
            var userDto = new UserCreateCommand("Hans", "h@h.com", "123");
            var result = await _client.PostAsJsonAsync(BaseAddress.User, userDto);
            
            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Fact]
        public async Task GetUsers_ShouldReturnNotFound_WhenNoUsersExist()
        {
            var results = await _client.GetAsync(BaseAddress.User);

            Assert.NotNull(results);
            Assert.Equal(HttpStatusCode.NotFound, results.StatusCode);
        }
    }
}