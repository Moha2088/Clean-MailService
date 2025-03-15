using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Domain.DomainEvents.Emails;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Emails.Create
{
    public sealed class SendEmalHandler : IRequestHandler<EmailCreateCommand, EmailGetResponseDto>
    {
        private readonly IEmailRepository _emailRepository;
        private readonly IPublisher _publisher;

        public SendEmalHandler(IEmailRepository emailRepository, IPublisher publisher)
        {
            _emailRepository = emailRepository;
            _publisher = publisher;
        }

        public async Task<EmailGetResponseDto> Handle(EmailCreateCommand request, CancellationToken cancellationToken)
        {
            var email = await _emailRepository.SendEmail(request, cancellationToken);
            await _publisher.Publish(new EmailSentEvent(email.Id, email.Subject));
            return email;
        }
    }
}
