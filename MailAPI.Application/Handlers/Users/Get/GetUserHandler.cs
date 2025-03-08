using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MediatR;

namespace MailAPI.Application.Handlers.Users.Get
{
    public sealed class GetUserHandler : IRequestHandler<UserGetRequestDto, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserGetResponseDto> Handle(UserGetRequestDto request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request, cancellationToken);
            return user;
        }
    }
}
