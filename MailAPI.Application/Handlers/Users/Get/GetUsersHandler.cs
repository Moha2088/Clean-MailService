using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MediatR;

namespace MailAPI.Application.Handlers.Users.Get
{
    public sealed class GetUsersHandler : IRequestHandler<UsersGetDto, List<UserGetResponseDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<List<UserGetResponseDto>> Handle(UsersGetDto request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetUsers(cancellationToken);
            return users;
        }
    }
}
