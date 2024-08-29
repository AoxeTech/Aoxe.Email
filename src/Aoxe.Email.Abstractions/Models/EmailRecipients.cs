namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailRecipients
{
    public List<EmailAddress> To { get; } = [];
    public List<EmailAddress> Cc { get; } = [];
    public List<EmailAddress> Bcc { get; } = [];
}
