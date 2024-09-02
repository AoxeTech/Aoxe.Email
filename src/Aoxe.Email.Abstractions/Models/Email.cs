namespace Aoxe.Email.Abstractions.Models;

public sealed class Email(string? id = null)
{
    public string Id { get; } = id is null ? Guid.NewGuid().ToString() : id.Trim();
    public EmailAddress From { get; set; } = new();
    public EmailAddress Sender { get; set; } = new();
    public List<EmailAddress> ReplyTo { get; set; } = [];
    public EmailRecipients Recipients { get; set; } = new();
    public EmailContent Content { get; set; } = new();
    public List<EmailAttachment> Attachments { get; set; } = [];

    public Email EmailFrom(string address, string? name = null)
    {
        From = new EmailAddress(address, name);
        return this;
    }

    public Email EmailSender(string address, string? name = null)
    {
        Sender = new EmailAddress(address, name);
        return this;
    }

    public Email EmailReplyTo(string address, string? name = null)
    {
        ReplyTo.Add(new EmailAddress(address, name));
        return this;
    }

    public Email EmailReplyTo(IEnumerable<EmailAddress> emailAddresses)
    {
        ReplyTo.AddRange(emailAddresses);
        return this;
    }

    public Email EmailTo(string address, string? name = null)
    {
        Recipients.To.Add(new EmailAddress(address, name));
        return this;
    }

    public Email EmailTo(IEnumerable<EmailAddress> emailAddresses)
    {
        Recipients.To.AddRange(emailAddresses);
        return this;
    }

    public Email EmailCc(string address, string? name = null)
    {
        Recipients.Cc.Add(new EmailAddress(address, name));
        return this;
    }

    public Email EmailCc(IEnumerable<EmailAddress> emailAddresses)
    {
        Recipients.Cc.AddRange(emailAddresses);
        return this;
    }

    public Email EmailBcc(string address, string? name = null)
    {
        Recipients.Bcc.Add(new EmailAddress(address, name));
        return this;
    }

    public Email EmailBcc(IEnumerable<EmailAddress> emailAddresses)
    {
        Recipients.Bcc.AddRange(emailAddresses);
        return this;
    }

    public Email Subject(string subject)
    {
        Content.Subject = subject;
        return this;
    }

    public Email TextBody(string textBody)
    {
        Content.TextBody = textBody;
        return this;
    }

    public Email HtmlBody(string htmlBody)
    {
        Content.HtmlBody = htmlBody;
        return this;
    }

    public Email Attach(
        string fileName,
        byte[] content,
        string mediaType = Defaults.MediaTypeApplication,
        string mediaSubType = Defaults.MediaSubTypeOctetStream
    )
    {
        Attachments.Add(new EmailAttachment(fileName, content, mediaType, mediaSubType));
        return this;
    }

    public Email Attach(
        IEnumerable<(string fileName, byte[] content)> attachments,
        string mediaType = Defaults.MediaTypeApplication,
        string mediaSubType = Defaults.MediaSubTypeOctetStream
    )
    {
        Attachments.AddRange(
            attachments.Select(x => new EmailAttachment(
                x.fileName,
                x.content,
                mediaType,
                mediaSubType
            ))
        );
        return this;
    }
}
