namespace Zaabee.Email.Abstractions.Contracts;

public class EmailRecipients
{
    public List<EmailAddress> To { get; set; } = new();
    public List<EmailAddress> Cc { get; set; } = new();
    public List<EmailAddress> Bcc { get; set; } = new();
}