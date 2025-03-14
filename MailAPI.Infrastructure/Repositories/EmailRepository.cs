using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Hangfire;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using MailAPI.Application.Interfaces.Email;
using MailAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MailAPI.Domain.Entities;
using Microsoft.Extensions.Logging;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Domain.Exceptions.User;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using MailAPI.Domain.Exceptions.Email;
using MailAPI.Application.Commands.Emails;

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

    public async Task<EmailGetResponseDto> SendEmail(EmailCreateCommand dto, CancellationToken cancellationToken)
    {
        var fromEmail = _secretClient.GetSecret("FROM-EMAIL").Value.Value;
        var appPassword = _secretClient.GetSecret("APP-PASSWORD").Value.Value;

        MailMessage message = new MailMessage()
        {
            Subject = dto.Subject,
            Body = dto.Body,
            IsBodyHtml = true,
            From = new MailAddress(fromEmail)
        };

        message.To.Add(new MailAddress(dto.To));

        var credentials = new NetworkCredential(fromEmail, appPassword);
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = credentials
        };

        var email = _mapper.Map<Email>(dto);

        var user = await _context.Users.FindAsync(dto.UserId, cancellationToken) ?? throw new UserNotFoundException();
        email.User = user;
        _context.Emails.Add(email);

        await _context.SaveChangesAsync(cancellationToken);
        await smtpClient.SendMailAsync(message);

        var mailJobId = _backgroundJobClient.Schedule(() => SendFollowUpMail(user.Id, new MailAddress(fromEmail), dto.To, credentials),
            TimeSpan.FromSeconds(30));

        _backgroundJobClient.ContinueJobWith(mailJobId, () => _logger.LogInformation("Follow up email has been sent!"));

        return _mapper.Map<EmailGetResponseDto>(email);
    }

    public async Task<EmailGetResponseDto> GetEmail(int id, CancellationToken cancellationToken)
    {
        var email = await _context.Emails
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken) ?? throw new EmailNotFoundException();

        return _mapper.Map<EmailGetResponseDto>(email);
    }

    public async Task<List<EmailGetResponseDto>> GetEmails(CancellationToken cancellationToken)
    {
        var emails = await _context.Emails
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return _mapper.Map<List<EmailGetResponseDto>>(emails);
    }

    public async Task SendFollowUpMail(int userId, MailAddress from, string to, NetworkCredential credentials)
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
            IsBodyHtml = true,
            From = from
        };

        followUpMessage.To.Add(new MailAddress(to));

        var followUpMail = new Email
        {
            To = to,
            Subject = subject,
            Body = body,
        };

        var user = await _context.Users.FindAsync(userId) ?? throw new UserNotFoundException();

        followUpMail.User = user;

        _context.Emails.Add(followUpMail);

        await _context.SaveChangesAsync();

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            EnableSsl = true,
            Credentials = credentials
        };

        await smtpClient.SendMailAsync(followUpMessage);
    }
}
