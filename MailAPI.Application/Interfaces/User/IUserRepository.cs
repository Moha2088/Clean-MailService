using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;

namespace MailAPI.Application.Interfaces.User;

public interface IUserRepository
{
    Task<UserGetResponseDto> CreateUser(UserCreateCommand dto, CancellationToken cancellationToken);
    Task<UserGetResponseDto> GetUser(UserGetQuery dto, CancellationToken cancellationToken);
    Task<List<UserGetResponseDto>> GetUsers(CancellationToken cancellationToken);
    Task<UserGetResponseDto> UpdateUser(UserUpdateCommand dto, CancellationToken cancellationToken);
    Task<UserGetResponseDto> DeleteUser(int id, CancellationToken cancellationToken);
}