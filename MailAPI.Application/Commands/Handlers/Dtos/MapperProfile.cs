using AutoMapper;
using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MailAPI.Application.Commands.Users;
using MailAPI.Domain.Entities;

namespace MailAPI.Application.Commands.Handlers.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateCommand, User>();
            CreateMap<User, UserGetResponseDto>();

            CreateMap<EmailCreateCommand, Email>();
            CreateMap<Email, EmailGetResponseDto>();

            CreateMap<UserUpdateCommand, User>();
        }
    }
}
