using MailAPI.Domain.Entities.Dtos.EmailDtos;

namespace MailAPI.Application.Interfaces.Email;
public interface IEmailRepository
{
    Task SendEmail(EmailCreateDto dto, CancellationToken cancellationToken);
    Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken);
    Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken);
}