using MailAPI.Domain.Entities.Dtos;
using MailAPI.Domain.Entities.Dtos.Email;

namespace MailAPI.Application.Interfaces.Email;
public interface IEmailRepository
{
    Task SendEmail(EmailCreateDto dto, CancellationToken cancellationToken);
    Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken);
    Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken);
}