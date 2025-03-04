namespace MailAPI.Domain.Exceptions.User;

/// <summary>
/// Exception to be thrown when attempting to create a user with an email that's already in use
/// </summary>
[Serializable]
public class UserExistsException : Exception
{
    public UserExistsException() { }
    
    public UserExistsException(string message) : base(message) { }
    
    public UserExistsException(string message, Exception innerException) : base(message, innerException) { }
}