using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Domain.DomainEvents.Emails
{
    public record EmailSentEvent(int Id, string Subject) : INotification;
}