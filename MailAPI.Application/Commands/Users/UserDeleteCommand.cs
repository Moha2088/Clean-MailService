using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Commands.Users
{
    public record UserDeleteCommand(int Id) : IRequest<DeleteUserResponseDto>;
}