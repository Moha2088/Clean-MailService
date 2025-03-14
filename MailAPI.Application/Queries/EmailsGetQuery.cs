using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Queries
{
    /// <summary>
    /// Record for the GetEmails-endpoint with no members, but is necessary since the MediatR requesthandler requires a request object
    /// </summary>
    public record EmailsGetQuery() : IRequest<List<EmailGetResponseDto>>;
}
