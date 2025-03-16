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
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ISender sender, ILogger<AuthenticationController> logger)
        {
            _sender = sender;
            _logger = logger;
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
                _logger.LogError(e.Message);
                return Unauthorized(e.Message);
            }
        }
    }
}