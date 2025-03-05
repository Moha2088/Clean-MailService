using MailAPI.Domain.Common;

namespace MailAPI.Domain.Entities;

public class Email : BaseEntity
{
    public string To { get; set; } = null!;
    
    public string Subject { get; set; } = null!;
    
    public string Body { get; set; } = null!;
    
    public int UserId { get; set; }
    
    public User? User { get; set; }
}