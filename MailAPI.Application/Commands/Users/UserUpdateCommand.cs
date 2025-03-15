using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MediatR;
using System.Text.Json.Serialization;

namespace MailAPI.Application.Commands.Users;

public record UserUpdateCommand([property: JsonIgnore] int Id, string Name, string Email, string Password) : IRequest<UserGetResponseDto>;