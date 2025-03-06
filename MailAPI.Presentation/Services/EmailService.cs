using MailAPI.Application.Interfaces.Email;
using MailAPI.Domain.Entities.Dtos.EmailDtos;

namespace MailAPI.Presentation.Services;

public class EmailService : IEmailService
{
    private readonly IEmailRepository _emailRepository;

    public EmailService(IEmailRepository emailRepository)
    {
        _emailRepository = emailRepository;
    }



    public Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SendEmail(EmailCreateDto dto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
