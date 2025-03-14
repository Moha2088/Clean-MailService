using MailAPI.Application.Handlers.Dtos.UserDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Queries
{
    /// <summary>
    /// Record for the GetUsers-endpoint with no members, but is necessary since the MediatR requesthandler requires a request object
    /// </summary>
    public record UsersGetQuery() :IRequest<List<UserGetResponseDto>>;
}