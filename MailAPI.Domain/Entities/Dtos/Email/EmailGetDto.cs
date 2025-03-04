namespace MailAPI.Domain.Entities.Dtos.Email;

public record EmailGetDto(string Id, string To, string Subject, string Body);