namespace MailAPI.Domain.Entities.Dtos.EmailDtos;

public record EmailGetDto(string Id, string To, string Subject, string Body);