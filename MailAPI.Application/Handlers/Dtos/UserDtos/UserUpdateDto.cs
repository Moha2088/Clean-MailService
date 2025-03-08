using MediatR;

namespace MailAPI.Application.Handlers.Dtos.UserDtos;

public record UserUpdateDto(string Name, string Email, string Password) : IRequest<int>;