﻿using MailAPI.Application.Commands.Handlers.Dtos.UserDtos;
using MediatR;

namespace MailAPI.Application.Queries.Users
{
    public record UserGetQuery(int Id) : IRequest<UserGetResponseDto>;
}
