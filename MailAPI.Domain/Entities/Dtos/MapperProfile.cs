using AutoMapper;
using MailAPI.Domain.Entities.Dtos.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Domain.Entities.Dtos
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserGetDto>();

        }
    }
}
