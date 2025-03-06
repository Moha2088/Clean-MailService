using MailAPI.Domain.Entities.Dtos.UserDtos;

namespace MailAPI.Application.Interfaces.User
{
    public interface IUserService
    {
        Task CreateUser(UserCreateDto dto, CancellationToken cancellationToken);
        Task<UserGetDto> GetUser(int id, CancellationToken cancellationToken);
        Task<List<UserGetDto>> GetUsers(CancellationToken cancellationToken);
        Task UpdateUser(int id, UserUpdateDto dto, CancellationToken cancellationToken);
        Task DeleteUser(int id, CancellationToken cancellationToken);
    }
}
