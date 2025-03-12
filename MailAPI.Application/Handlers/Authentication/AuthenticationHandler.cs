using MailAPI.Application.Handlers.Dtos;
using MailAPI.Application.Interfaces;
using MailAPI.Domain.Exceptions.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Handlers.Authentication
{
    public sealed class AuthenticationHandler : IRequestHandler<AuthenticationDto, TokenDto>
    {
        private readonly IAuthenticationService _authService;

        public AuthenticationHandler(IAuthenticationService authService)
        {
            _authService = authService;
        }

        public async Task<TokenDto> Handle(AuthenticationDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _authService.AuthenticateUser(dto, cancellationToken);
                return token;
            }

            catch (UserNotFoundException)
            {
                throw;
            }
        }
    }
}
