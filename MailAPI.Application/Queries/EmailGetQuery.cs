using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Queries
{
    public record EmailGetQuery(int Id) : IRequest<EmailGetResponseDto>;
}
