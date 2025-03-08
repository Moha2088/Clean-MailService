using MediatR;

namespace MailAPI.Application.Handlers.Dtos.EmailDtos;

public record EmailGetDto(string Id, string To, string Subject, string Body) : IRequest<int>;