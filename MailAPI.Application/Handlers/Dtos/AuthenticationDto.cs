﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Handlers.Dtos
{
    public record AuthenticationDto(string Email, string Password) : IRequest<TokenDto>;
}
