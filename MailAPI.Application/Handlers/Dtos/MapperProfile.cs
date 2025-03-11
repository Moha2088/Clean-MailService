using AutoMapper;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Handlers.Dtos.UserDtos;
using MailAPI.Domain.Entities;

namespace MailAPI.Application.Handlers.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserGetResponseDto>();

            CreateMap<EmailCreateDto, Email>();
            CreateMap<Email, EmailGetResponseDto>();
        }
    }
}
