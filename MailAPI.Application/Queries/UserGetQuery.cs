using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Queries
{
    public record UserGetQuery(int Id) : IRequest<UserGetResponseDto>;
}
