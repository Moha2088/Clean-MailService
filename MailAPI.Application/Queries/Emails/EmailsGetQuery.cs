using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MediatR;

namespace MailAPI.Application.Queries.Emails
{
    /// <summary>
    /// Record for the GetEmails-endpoint with no members, but is necessary since the MediatR requesthandler requires a request object
    /// </summary>
    public record EmailsGetQuery(int Id) : IRequest<List<EmailGetResponseDto>>;
}
