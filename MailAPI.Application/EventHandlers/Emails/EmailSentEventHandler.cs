using MailAPI.Domain.DomainEvents.Emails;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MailAPI.Application.EventHandlers.Emails
{
    public sealed class EmailSentEventHandler : INotificationHandler<EmailSentEvent>
    {
        private readonly ILogger<EmailSentEventHandler> _logger;

        public EmailSentEventHandler(ILogger<EmailSentEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmailSentEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Email sent, Id: {notification.Id}, Subject: {notification.Subject}");
            return Task.CompletedTask;
        }
    }
}