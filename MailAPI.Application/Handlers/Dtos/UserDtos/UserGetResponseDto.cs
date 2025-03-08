using MediatR;

namespace MailAPI.Application.Handlers.Dtos.UserDtos;

public record UserGetResponseDto(string Id, string Name, string Email) : IRequest<UserGetResponseDto>;