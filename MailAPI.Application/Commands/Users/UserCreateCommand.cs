using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Commands.Users;

public record UserCreateCommand(string Name, string Email, string Password) : IRequest<UserGetResponseDto>;