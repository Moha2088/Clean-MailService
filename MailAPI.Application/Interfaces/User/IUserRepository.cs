using MailAPI.Application.Commands.Users;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;

namespace MailAPI.Application.Interfaces.User;

public interface IUserRepository
{
    Task<UserGetResponseDto> CreateUser(UserCreateCommand dto, CancellationToken cancellationToken);
    Task<UserGetResponseDto> GetUser(UserGetQuery dto, CancellationToken cancellationToken);
    Task<List<UserGetResponseDto>> GetUsers(CancellationToken cancellationToken);
    Task UpdateUser(int id, UserUpdateCommand dto, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
}