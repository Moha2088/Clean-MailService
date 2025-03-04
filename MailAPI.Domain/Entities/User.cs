namespace MailAPI.Domain.Entities;

public class User
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    public string Password { get; set; } = null!;
    
    public ICollection<Email> Emails { get; set; } = null!;
}
