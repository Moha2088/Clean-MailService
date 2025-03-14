using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MediatR;

namespace MailAPI.Application.Queries.Emails
{
    public record EmailGetQuery(int Id) : IRequest<EmailGetResponseDto>;
}
