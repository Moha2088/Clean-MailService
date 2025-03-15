using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.DomainEvents.Users;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Users.Delete
{
    public sealed class DeleteUserHandler : IRequestHandler<UserDeleteCommand, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublisher _publisher;

        public DeleteUserHandler(IUserRepository userRepository, IPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task<UserGetResponseDto> Handle(UserDeleteCommand dto, CancellationToken cancellationToken)
        {
            var user = await _userRepository.DeleteUser(dto.Id, cancellationToken);
            await _publisher.Publish(new UserDeletedEvent(user.Id, user.Name));
            return user;
        }
    }
}
