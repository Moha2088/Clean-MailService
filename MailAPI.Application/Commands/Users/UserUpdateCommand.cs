using MediatR;

namespace MailAPI.Application.Commands.Users;

public record UserUpdateCommand(string Name, string Email, string Password) : IRequest<int>;