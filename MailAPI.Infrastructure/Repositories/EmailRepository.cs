using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Hangfire;
using MailAPI.Domain.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using MailAPI.Application.Interfaces;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Domain.Entities.Dtos.EmailDtos;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MailAPI.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace MailAPI.Infrastructure.Repositories;
public class EmailRepository : IEmailRepository
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private SecretClient _secretClient;
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<EmailRepository> _logger;

    public EmailRepository(DataContext context, IMapper mapper, IBackgroundJobClient backgroundJobClient, ILogger<EmailRepository> logger)
    {
        _context = context;
        _mapper = mapper;
        _backgroundJobClient = backgroundJobClient;
        _logger = logger;

        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var uri = new Uri(config["Azure:VaultUri"]!);
        _secretClient = new SecretClient(uri, new DefaultAzureCredential());
        _backgroundJobClient = backgroundJobClient;
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

        _context.Emails.Add(new Email
        {
            To = dto.To,
            Subject = dto.Subject,
            Body = dto.Body
        });

        await _context.SaveChangesAsync(cancellationToken);
        await smtpClient.SendMailAsync(message, cancellationToken);

        var mailJobId = _backgroundJobClient.Schedule(() => SendFollowUpMail(fromEmail, dto.To, smtpClient),
            TimeSpan.FromSeconds(30));

        _backgroundJobClient.ContinueJobWith(mailJobId, () => _logger.LogInformation("Follow up email has been sent!"));
    }

    public async Task<EmailGetDto> GetEmail(int id, CancellationToken cancellationToken)
    {
        var email = await _context.Emails
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);

        return _mapper.Map<EmailGetDto>(email);
    }

    public async Task<List<EmailGetDto>> GetEmails(CancellationToken cancellationToken)
    {
        var emails = await _context.Emails
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmailGetDto>>(emails);
    }

    private async Task SendFollowUpMail(string from, string to, SmtpClient smtpClient)
    {
        var subject = "Continuation Email";
        var body = "<p>Hello</p>" +
                   "<p>This is a continuation of the previous email</p> " +
                   "<p>Kind Regards</p>" +
                   "<p>Mohamed</p";

        MailMessage followUpMessage = new MailMessage()
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        followUpMessage.To.Add(new MailAddress(to));

        _context.Emails.Add(new Email
        {
            To = to,
            Subject = subject,
            Body = body
        });

        await _context.SaveChangesAsync();

        await smtpClient.SendMailAsync(followUpMessage);
    }
}
