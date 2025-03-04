namespace MailAPI.Domain.Entities;

public class Email
{
    public int Id { get; set; }

    public string To { get; set; } = null!;
    
    public string Subject { get; set; } = null!;
    
    public string Body { get; set; } = null!;
    
    public int UserId { get; set; }
    
    public User? User { get; set; }
}
