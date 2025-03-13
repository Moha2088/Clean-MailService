using System.Net;
using System.Net.Http.Json;
using MailAPI.Domain.Entities;
using MailAPI.Infrastructure.Data;
using MailAPI.Tests.IntegrationTests.Common;
using MailAPI.Tests.IntegrationTests.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace MailAPI.Tests.IntegrationTests;

public class AuthenticationController : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly DataContext _context;
    public AuthenticationController(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
        var scope = factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<DataContext>();
    }
    
    [Fact]
    public async Task Login_ShouldReturnOK_WhenUserExists()
    {
        var user = new User
        {
            Name = "Test",
            Email = "test@example.com",
            Password = "Test"
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _client.PostAsJsonAsync($"{BaseAddress.Auth}/login", user);
        
        result.EnsureSuccessStatusCode();
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}