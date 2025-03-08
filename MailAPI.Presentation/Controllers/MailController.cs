using MailAPI.Application.Handlers.Dtos.EmailDtos;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.Presentation.Controllers;

[ApiController]
[Route("api/mails")]
public class MailController : ControllerBase
{
    public MailController() { }



    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailCreateDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }
}
