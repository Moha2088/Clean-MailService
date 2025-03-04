using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.Entities.Dtos.User;

namespace MailAPI.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    public Task CreateUser(UserCreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserGetDto> GetUser(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserGetDto>> GetUsers(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUser(UserUpdateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(int userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}