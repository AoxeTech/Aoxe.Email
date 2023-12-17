namespace Zaabee.Email.Abstractions.Models;

public class Email
{
    public string Id { get; }
    public EmailAddress From { get; set; } = new();
    public EmailAddress Sender { get; set; } = new();
    public List<EmailAddress> ReplyTo { get; set; } = [];
    public EmailRecipients Recipients { get; set; } = new();
    public EmailContent Content { get; set; } = new();
    public List<EmailAttachment> Attachments { get; set; } = [];

    public Email()
    {
        Id = SequentialGuidHelper.GenerateComb().ToString();
    }

    public Email(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException(
                $"{nameof(id)} can not be null or empty or white space."
            );
        Id = id.Trim();
    }
}
