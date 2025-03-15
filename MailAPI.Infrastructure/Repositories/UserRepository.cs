using AutoMapper;
using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Interfaces.User;
using MailAPI.Application.Queries;
using MailAPI.Application.Queries.Users;
using MailAPI.Domain.Entities;
using MailAPI.Domain.Exceptions.User;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

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
    
    public async Task<UserGetResponseDto> CreateUser(UserCreateCommand dto, CancellationToken cancellationToken)
    {
        if (_context.Users.Any(user => user.Email.Equals(dto.Email)))
        {
            throw new UserExistsException();
        }
        
        var user = _mapper.Map<User>(dto);

        string passwordHash = BC.HashPassword(dto.Password);
        user.Password = passwordHash;
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UserGetResponseDto>(user);
    }

    public async Task<UserGetResponseDto> GetUser(UserGetQuery dto, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals((dto.Id)), cancellationToken) ?? 
                   throw new UserNotFoundException();

        return _mapper.Map<UserGetResponseDto>(user);
    }

    public async Task<List<UserGetResponseDto>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<UserGetResponseDto>>(users);
    }

    public async Task UpdateUser(int id, UserUpdateCommand dto, CancellationToken cancellationToken)
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