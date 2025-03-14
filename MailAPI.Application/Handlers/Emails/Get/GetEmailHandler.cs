

using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Application.Queries;
using MailAPI.Domain.Exceptions.Email;
using MediatR;

namespace MailAPI.Application.Handlers.Emails.Get
{
    public sealed class GetEmailHandler : IRequestHandler<EmailGetQuery, EmailGetResponseDto>
    {
        private readonly IEmailRepository _emailRepository;

        public GetEmailHandler(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }


        public async Task<EmailGetResponseDto> Handle(EmailGetQuery dto, CancellationToken cancellationToken)
        {
            try
            {
                var email = await _emailRepository.GetEmail(dto.Id, cancellationToken);
                return email;
            }

            catch (EmailNotFoundException)
            {
                throw;
            }
        }
    }
}