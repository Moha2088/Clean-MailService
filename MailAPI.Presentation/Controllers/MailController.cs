using MailAPI.Application.Handlers.Dtos.EmailDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers;

[ApiController]
[Route("api/mails")]
public class MailController : ControllerBase
{
    private readonly IMediator _mediator;

    public MailController(IMediator mediator) 
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
    public async Task<IActionResult> SendEmail([FromBody] EmailCreateDto dto, CancellationToken cancellationToken)
    {
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
        var requestDto = new EmailGetRequestDto(Id: id);
        var email = await _mediator.Send(requestDto, cancellationToken);
        return Ok(email);
    }

    /// <summary>
    /// Gets a list of emails
    /// </summary>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEmails(CancellationToken cancellationToken)
    {
        var emails = await _mediator.Send(new EmailsGetDto(), cancellationToken);
        return emails.Any() ? Ok() : NotFound();
    }
}
