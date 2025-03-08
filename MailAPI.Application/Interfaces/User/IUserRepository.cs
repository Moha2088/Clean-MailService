using MailAPI.Application.Handlers.Dtos.UserDtos;

namespace MailAPI.Application.Interfaces.User;

public interface IUserRepository
{
    Task<int> CreateUser(UserCreateDto dto, CancellationToken cancellationToken);
    Task<UserGetResponseDto> GetUser(UserGetRequestDto dto, CancellationToken cancellationToken);
    Task<List<UserGetResponseDto>> GetUsers(CancellationToken cancellationToken);
    Task UpdateUser(int id, UserUpdateDto dto, CancellationToken cancellationToken);
    Task DeleteUser(int id, CancellationToken cancellationToken);
}