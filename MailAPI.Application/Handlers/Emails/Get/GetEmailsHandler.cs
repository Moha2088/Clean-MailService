using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Handlers.Emails.Get
{
    public sealed class GetEmailsHandler : IRequestHandler<EmailsGetDto, List<EmailGetResponseDto>>
    {
        private readonly IEmailRepository _emailRepository;

        public GetEmailsHandler(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }


        public async Task<List<EmailGetResponseDto>> Handle(EmailsGetDto request, CancellationToken cancellationToken)
        {
            var emails = await _emailRepository.GetEmails(cancellationToken);
            return emails;
        }
    }
}
