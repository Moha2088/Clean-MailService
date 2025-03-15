using MediatR;

namespace MailAPI.Application.Commands.Handlers.Dtos.UserDtos;

public record UserGetResponseDto(int Id, string Name, string Email) : IRequest<UserGetResponseDto>;