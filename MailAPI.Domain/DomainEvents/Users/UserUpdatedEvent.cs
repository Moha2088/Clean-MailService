using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Domain.DomainEvents.Users;

public record UserUpdatedEvent(int Id, string Name) : INotification;

