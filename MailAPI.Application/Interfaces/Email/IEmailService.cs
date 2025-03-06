using MailAPI.Domain.Entities.Dtos.EmailDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendEmail(EmailCreateDto dto, CancellationToken cancellationToken);
        Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken);
        Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken);
    }
}
