using MailAPI.Application.Commands.Handlers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthenticationController(IMediator mediator)
        {
            _sender = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _sender.Send(dto, cancellationToken);
                return Ok(result.Token);
            }

            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
        }
    }
}
