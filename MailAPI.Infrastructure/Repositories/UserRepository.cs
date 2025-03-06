using AutoMapper;
using MailAPI.Application.Interfaces.User;
using MailAPI.Domain.Entities;
using MailAPI.Domain.Entities.Dtos.UserDtos;
using MailAPI.Domain.Exceptions.User;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MailAPI.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserRepository(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task CreateUser(UserCreateDto dto, CancellationToken cancellationToken)
    {
        if (_context.Users.Any(user => user.Email.Equals(dto.Email)))
        {
            throw new UserExistsException();
        }
        
        var user = _mapper.Map<User>(dto);
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserGetDto> GetUser(int userId, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals((userId)), cancellationToken) ?? 
                   throw new UserNotFoundException();

        return _mapper.Map<UserGetDto>(user);
    }

    public async Task<List<UserGetDto>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserGetDto>>(users);
    }

    public async Task UpdateUser(int id, UserUpdateDto dto, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new UserNotFoundException();
        _mapper.Map(user, dto);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(id) ?? throw new UserNotFoundException();
        await _context.SaveChangesAsync(cancellationToken);
    }
}