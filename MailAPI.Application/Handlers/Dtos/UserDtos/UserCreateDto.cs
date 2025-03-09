using MediatR;

namespace MailAPI.Application.Handlers.Dtos.UserDtos;

public record UserCreateDto(string Name, string Email, string Password) : IRequest<UserGetResponseDto>;