namespace Zaabee.Email.Abstractions.Models;

public class EmailRecipients
{
    public List<EmailAddress> To { get; set; } = new();
    public List<EmailAddress> Cc { get; set; } = new();
    public List<EmailAddress> Bcc { get; set; } = new();
}