using MailAPI.Application.Handlers.Dtos.EmailDtos;

namespace MailAPI.Application.Interfaces.Email;
public interface IEmailRepository
{
    Task<EmailGetResponseDto> SendEmail(EmailCreateDto dto, CancellationToken cancellationToken);
    Task<EmailGetResponseDto> GetEmail(int id, CancellationToken cancellationToken);
    Task<List<EmailGetResponseDto>> GetEmails(CancellationToken cancellationToken);
}