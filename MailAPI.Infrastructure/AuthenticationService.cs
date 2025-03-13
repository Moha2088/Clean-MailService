using MailAPI.Application.Handlers.Dtos;
using MailAPI.Application.Interfaces;
using MailAPI.Domain.Entities;
using MailAPI.Domain.Exceptions.User;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace MailAPI.Infrastructure;

public class AuthenticationService : IAuthenticationService
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public AuthenticationService(DataContext context)
    {
        _context = context;
        _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }

    public async Task<TokenDto>AuthenticateUser(AuthenticationDto dto, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Email.Equals(dto.Email) && x.Password.Equals(dto.Password), 
                cancellationToken) ?? throw new UserNotFoundException();

        var token = GenerateToken(user);
        return new TokenDto(Token: token);
    }


    public string GenerateToken(User user)
    {
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]!));
        var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JWTSettings:Issuer"],
            audience: _config["JWTSettings:Audience"],
            claims: new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString())
            },

            expires: DateTime.Now.AddHours(1),
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}