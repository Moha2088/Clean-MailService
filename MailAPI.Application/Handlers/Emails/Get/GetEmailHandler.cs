

using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MediatR;

namespace MailAPI.Application.Handlers.Emails.Get
{
    public sealed class GetEmailHandler : IRequestHandler<EmailGetRequestDto, EmailGetResponseDto>
    {
        private readonly IEmailRepository _emailRepository;

        public GetEmailHandler(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }


        public async Task<EmailGetResponseDto> Handle(EmailGetRequestDto dto, CancellationToken cancellationToken)
        {
            var email = await _emailRepository.GetEmail(dto.Id, cancellationToken);
            return email;
        }
    }
}