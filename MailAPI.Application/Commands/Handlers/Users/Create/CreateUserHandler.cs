using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Events.Users;
using MailAPI.Application.Interfaces.User;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Users.Create
{
    public sealed class CreateUserHandler : IRequestHandler<UserCreateCommand, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublisher _publisher;

        public CreateUserHandler(IUserRepository userRepository, IPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task<UserGetResponseDto> Handle(UserCreateCommand dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CreateUser(dto,cancellationToken);
            await _publisher.Publish(new UserCreatedEvent(user.Id, user.Name), cancellationToken);
            return user;
        }
    }
}