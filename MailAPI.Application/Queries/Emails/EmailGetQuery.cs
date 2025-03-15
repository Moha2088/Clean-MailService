using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MediatR;

namespace MailAPI.Application.Queries.Emails
{
    public record EmailGetQuery(int Id) : IRequest<EmailGetResponseDto>;
}
