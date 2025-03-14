using MailAPI.Application.Commands.Emails;
using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MailAPI.Application.Queries;
using MailAPI.Domain.Exceptions.Email;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MailAPI.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/mails")]
public class EmailController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmailController(IMediator mediator) 
    {
        _mediator = mediator;
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
        var email = await _mediator.Send(dto, cancellationToken);
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
            var email = await _mediator.Send(requestDto, cancellationToken);
            return Ok(email);
        }

        catch(EmailNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    /// <summary>
    /// Gets a list of emails
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEmails(CancellationToken cancellationToken)
    {
        var emails = await _mediator.Send(new EmailsGetQuery(), cancellationToken);
        return emails.Any() ? Ok() : NotFound();
    }
}
