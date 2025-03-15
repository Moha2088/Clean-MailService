using MailAPI.Domain.DomainEvents.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MailAPI.Application.EventHandlers.Users
{
    public sealed class UserDeletedEventHandler : INotificationHandler<UserDeletedEvent>
    {
        private readonly ILogger<UserDeletedEventHandler> _logger;

        public UserDeletedEventHandler(ILogger<UserDeletedEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(UserDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"User deleted, Id: {notification.Id} Name: {notification.Name}");
            return Task.CompletedTask;
        }
    }
}