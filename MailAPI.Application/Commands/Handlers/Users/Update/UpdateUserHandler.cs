using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.DomainEvents.Users;
using MailAPI.Domain.Exceptions.User;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Users.Update
{
    public sealed class UpdateUserHandler : IRequestHandler<UserUpdateCommand, UserGetResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPublisher _publisher;

        public UpdateUserHandler(IUserRepository userRepository, IPublisher publisher)
        {
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public async Task<UserGetResponseDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.UpdateUser(request, cancellationToken);
                await _publisher.Publish(new UserUpdatedEvent(request.Id, request.Name));
                return user;
            }

            catch (UserNotFoundException)
            {
                throw;
            }
        }
    }
}
