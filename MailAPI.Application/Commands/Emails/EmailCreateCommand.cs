using MailAPI.Application.Commands.Handlers.Dtos.EmailDtos;
using MediatR;
using System.Text.Json.Serialization;

namespace MailAPI.Application.Commands.Emails;

public class EmailCreateCommand : IRequest<EmailGetResponseDto>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
