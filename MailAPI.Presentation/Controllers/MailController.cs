using MailAPI.Domain.Entities.Dtos;
using MailAPI.Domain.Entities.Dtos.Email;
using Microsoft.AspNetCore.Mvc;

namespace MailAPI.API.Controllers;

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
