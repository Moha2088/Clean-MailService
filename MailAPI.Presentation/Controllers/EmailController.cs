using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Queries;
using MailAPI.Domain.Exceptions.Email;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MailAPI.Application.Queries.Emails;
using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;

namespace MailAPI.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/mails")]
public class EmailController : ControllerBase
{
    private readonly ISender _sender;
    private readonly ILogger<EmailController> _logger;

    public EmailController(ISender sender, ILogger<EmailController> logger)
    {
        _sender = sender;
        _logger = logger;
    }


    /// <summary>
    /// Sends an email
    /// </summary>
    /// <param name="dto">Dto for sending an email</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code= "201">Returns Created</response>
    [HttpPost]
    [ProducesResponseType(typeof(EmailGetResponseDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> SendEmail([FromBody] EmailCreateCommand dto, CancellationToken cancellationToken)
    {
        int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var authenticatedUserId);
        dto.UserId = authenticatedUserId;
        var email = await _sender.Send(dto, cancellationToken);
        return Ok(email);
    }

    /// <summary>
    /// Gets an email
    /// </summary>
    /// <param name="id">Id of the email</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code= "200">Returns OK</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EmailGetResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEmail([FromRoute] int id, CancellationToken cancellationToken)
    {
        var requestDto = new EmailGetQuery(Id: id);
        try
        {
            var email = await _sender.Send(requestDto, cancellationToken);
            return Ok(email);
        }

        catch (EmailNotFoundException e)
        {
            _logger.LogError(e.Message);
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Gets a list of emails
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <response code="200">Returns OK with Emails</response>
    /// <response code="404">Returns NotFound if no emails exist</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmails(CancellationToken cancellationToken)
    {
        int.TryParse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
        var emails = await _sender.Send(new EmailsGetQuery(Id: userId), cancellationToken);
        return emails.Any() ? Ok(emails) : NotFound();
    }
}