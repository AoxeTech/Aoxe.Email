namespace Zaabee.Email.Abstractions.Contracts;

public class SendEmailCommand
{
    public string Id { get;}
    public EmailAddress From { get; set; } = new();
    public EmailAddress Sender { get; set; } = new();
    public List<EmailAddress> ReplyTo { get; set; } = new();
    public EmailRecipients Recipients { get; set; } = new();
    public EmailContent Content { get; set; } = new();

    public SendEmailCommand()
    {
        Id = SequentialGuidHelper.GenerateComb().ToString();
    }

    public SendEmailCommand(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ArgumentNullException($"{nameof(id)} can not be null or empty or white space.");
        Id = id.Trim();
    }
}