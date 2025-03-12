using MailAPI.Application.Handlers.Dtos;
using MailAPI.Domain.Entities;

namespace MailAPI.Application.Interfaces;

public interface IAuthenticationService
{
    string GenerateToken(Domain.Entities.User user);
    Task<TokenDto> AuthenticateUser(AuthenticationDto dto, CancellationToken cancellationToken);
}