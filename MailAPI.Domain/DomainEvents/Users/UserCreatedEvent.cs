using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailAPI.Application.Events.Users;

public record UserCreatedEvent(int Id, string Name) : INotification;
