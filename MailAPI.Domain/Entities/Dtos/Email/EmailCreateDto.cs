namespace MailAPI.Domain.Entities.Dtos.Email;

public record EmailCreateDto(string To, string Subject, string Body);