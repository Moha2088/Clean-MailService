using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Users.Get
{
    public sealed class GetUserHandler : IRequestHandler<UserGetQuery, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserGetResponseDto> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request, cancellationToken);
            return user;
        }
    }
}
