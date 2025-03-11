using MediatR;

namespace MailAPI.Application.Handlers.Dtos.EmailDtos;

public record EmailCreateDto(string To, string Subject, string Body) : IRequest<EmailGetResponseDto>;