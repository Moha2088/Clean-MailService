using MailAPI.Application.Events.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MailAPI.Application.EventHandlers.Users
{
    public sealed class UserCreatedEventHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"User created, Id: {notification.Id}, Name: {notification.Name}");
            return Task.CompletedTask;
        }
    }
}