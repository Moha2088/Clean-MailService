using MailAPI.Domain.DomainEvents.Users;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MailAPI.Application.EventHandlers.Users;

public sealed class UserUpdatedEventHandler : INotificationHandler<UserUpdatedEvent>
{
    private readonly ILogger<UserUpdatedEventHandler> _logger;

    public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Updated user, Id: {notification.Id} Name: {notification.Name}");
        return Task.CompletedTask;
    }
}