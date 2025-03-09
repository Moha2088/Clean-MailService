using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MediatR;

namespace MailAPI.Application.Handlers.Users.Create
{
    public sealed class CreateUserHandler : IRequestHandler<UserCreateDto, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserGetResponseDto> Handle(UserCreateDto dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CreateUser(dto,cancellationToken);
            return user;
        }
    }
}