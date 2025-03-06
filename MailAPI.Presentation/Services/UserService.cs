using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.Entities.Dtos.UserDtos;

namespace MailAPI.Presentation.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task CreateUser(UserCreateDto dto, CancellationToken cancellationToken)
    {
        await _userRepository.CreateUser(dto, cancellationToken);
    }

    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteUser(id, cancellationToken);
    }

    public async Task<UserGetDto> GetUser(int id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUser(id, cancellationToken);
        return user;
    }

    public async Task<List<UserGetDto>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetUsers(cancellationToken);
        return users;
    }

    public async Task UpdateUser(int id, UserUpdateDto dto, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateUser(id, dto, cancellationToken);
    }
}
