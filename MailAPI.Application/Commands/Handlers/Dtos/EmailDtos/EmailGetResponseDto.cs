using MediatR;

namespace MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;

public record EmailGetResponseDto(int Id, string To, string Subject, string Body) : IRequest<int>;