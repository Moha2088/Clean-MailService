using MailAPI.Domain.Entities;

namespace MailAPI.Domain.Exceptions.Email;

/// <summary>
/// Exception to be thrown when a <see cref="Email"/>
/// </summary>
[Serializable]
public class EmailNotFoundException : Exception
{
    public EmailNotFoundException() { }

    public EmailNotFoundException(string message): base(message) { }
    
    public EmailNotFoundException(string message, Exception innerException): base(message, innerException) { }
}
