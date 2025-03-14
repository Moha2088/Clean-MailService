using MailAPI.Application.Commands.Users;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Interfaces.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Handlers.Users.Delete
{
    public sealed class DeleteUserHandler : IRequestHandler<UserDeleteCommand, DeleteUserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DeleteUserResponseDto> Handle(UserDeleteCommand dto, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteUser(dto.Id, cancellationToken);
            return new DeleteUserResponseDto();
        }
    }
}
