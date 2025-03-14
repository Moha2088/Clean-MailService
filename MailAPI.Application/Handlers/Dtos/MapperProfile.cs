using AutoMapper;
using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Commands.Users;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Domain.Entities;

namespace MailAPI.Application.Handlers.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateCommand, User>();
            CreateMap<User, UserGetResponseDto>();

            CreateMap<EmailCreateCommand, Email>();
            CreateMap<Email, EmailGetResponseDto>();
        }
    }
}
