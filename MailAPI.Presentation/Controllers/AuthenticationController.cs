using MailAPI.Application.Handlers.Dtos;
using MailAPI.Domain.Exceptions.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _mediator.Send(dto, cancellationToken);
                return Ok(result.Token);
            }

            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
