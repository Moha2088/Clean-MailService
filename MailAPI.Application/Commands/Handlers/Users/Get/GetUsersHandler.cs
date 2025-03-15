using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;
using MediatR;

namespace MailAPI.Application.Commands.Handlers.Users.Get
{
    public sealed class GetUsersHandler : IRequestHandler<UsersGetQuery, List<UserGetResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<List<UserGetResponseDto>> Handle(UsersGetQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsers(cancellationToken);
            return users;
        }
    }
}
