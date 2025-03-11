using MediatR;

namespace MailAPI.Application.Handlers.Dtos.EmailDtos;

public record EmailGetResponseDto(string Id, string To, string Subject, string Body) : IRequest<int>;