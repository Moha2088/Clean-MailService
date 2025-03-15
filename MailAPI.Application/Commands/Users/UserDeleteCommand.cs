using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Commands.Users
{
    public record UserDeleteCommand(int Id) : IRequest<UserGetResponseDto>;
}