using MailAPI.Domain.Entities;

namespace MailAPI.Domain.Exceptions.User;

/// <summary>
/// Exception to be thrown when a <see cref="User"/> can't be found by the provided id
/// </summary>

[Serializable]
public class UserNotFoundException : Exception
{
    public UserNotFoundException() { }

    public UserNotFoundException(string message) : base(message) { }

    public UserNotFoundException(string message, Exception innerException) : base(message, innerException) { }
}