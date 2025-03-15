using MediatR;

namespace MailAPI.Domain.DomainEvents.Users;

public record UserDeletedEvent(int Id, string Name) : INotification;