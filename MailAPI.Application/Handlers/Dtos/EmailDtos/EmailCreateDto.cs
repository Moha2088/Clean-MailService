using MediatR;
using System.Text.Json.Serialization;

namespace MailAPI.Application.Handlers.Dtos.EmailDtos;

public class EmailCreateDto : IRequest<EmailGetResponseDto>
{
    [JsonIgnore]
    public int UserId { get; set; }
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
}
