using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MediatR;

namespace MailAPI.Application.Handlers.Emails.Create
{
    public sealed class SendEmalHandler : IRequestHandler<EmailCreateDto, EmailGetResponseDto>
    {
        private readonly IEmailRepository _emailRepository;

        public SendEmalHandler(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<EmailGetResponseDto> Handle(EmailCreateDto request, CancellationToken cancellationToken)
        {
            var email = await _emailRepository.SendEmail(request, cancellationToken);
            return email;
        }
    }
}
