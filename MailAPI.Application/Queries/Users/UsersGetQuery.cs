using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Queries.Users
{
    /// <summary>
    /// Record for the GetUsers-endpoint with no members, but is necessary since the MediatR requesthandler requires a request object
    /// </summary>
    public record UsersGetQuery() :IRequest<List<UserGetResponseDto>>;
}