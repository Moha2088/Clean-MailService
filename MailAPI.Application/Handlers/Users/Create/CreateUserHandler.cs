using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MediatR;

namespace MailAPI.Application.Handlers.Users.Create
{
    public sealed class CreateUserHandler : IRequestHandler<UserCreateDto, int>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(UserCreateDto dto, CancellationToken cancellationToken)
        {
            var userId = await _userRepository.CreateUser(dto,cancellationToken);
            return userId;
        }
    }
}