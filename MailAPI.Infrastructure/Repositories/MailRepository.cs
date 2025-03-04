using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Hangfire;
using MailAPI.Domain.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using MailAPI.Application.Interfaces;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Domain.Entities.Dtos.Email;

namespace MailAPI.Infrastructure.Repositories;
public class MailRepository : IEmailRepository
{
    private SecretClient _secretClient;

    public MailRepository()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var uri = new Uri(config["Azure:VaultUri"]!);
        _secretClient = new SecretClient(uri, new DefaultAzureCredential());
    }

    public async Task SendEmail(EmailCreateDto dto, CancellationToken cancellationToken)
    {
        var fromEmail = _secretClient.GetSecret("FROM-EMAIL").Value.Value;
        var appPassword = _secretClient.GetSecret("APP.PASSWORD").Value.Value;

        MailMessage message = new MailMessage()
        {
            Subject = dto.Subject,
            Body = dto.Body,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(dto.To));

        var credentials = new NetworkCredential(fromEmail, appPassword);

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = credentials
        };

        await smtpClient.SendMailAsync(message, cancellationToken);
    }

    public Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private async Task SendFollowUp(string from, string to, SmtpClient smtpClient)
    {
        MailMessage followUpMessage = new MailMessage()
        {
            Subject = "Opfølgning på ansøgning",
            Body = "Hej. Jeg skriver igen for at få status på min ansøgning som jeg har sendt tidligere på ugen.",
            IsBodyHtml = true
        };

        followUpMessage.To.Add(new MailAddress(to));

        await smtpClient.SendMailAsync(followUpMessage);
    }
}
