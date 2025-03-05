using MailAPI.Domain.Common;

namespace MailAPI.Domain.Entities;

public class User : BaseEntity
{   
    public string Name { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public ICollection<Email> Emails { get; set; } = null!;
}