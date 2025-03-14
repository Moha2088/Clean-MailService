using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Application.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Handlers.Emails.Get
{
    public sealed class GetEmailsHandler : IRequestHandler<EmailsGetQuery, List<EmailGetResponseDto>>
    {
        private readonly IEmailRepository _emailRepository;

        public GetEmailsHandler(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }


        public async Task<List<EmailGetResponseDto>> Handle(EmailsGetQuery request, CancellationToken cancellationToken)
        {
            var emails = await _emailRepository.GetEmails(cancellationToken);
            return emails;
        }
    }
}
