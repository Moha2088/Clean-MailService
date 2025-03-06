namespace MailAPI.Domain.Entities.Dtos.EmailDtos;

public record EmailCreateDto(string To, string Subject, string Body);