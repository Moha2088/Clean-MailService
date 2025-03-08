
using MediatR;

namespace MailAPI.Application.Handlers.Dtos.UserDtos
{
    public record UserGetRequestDto(int Id) : IRequest<UserGetResponseDto>;
}
