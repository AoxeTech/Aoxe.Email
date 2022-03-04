namespace Zaabee.MailKit;

public class Mail
{
    public string? GetFrom { get; private set; }

    public string? GetSubject { get; private set; }

    public string? GetBody { get; private set; }

    public MessagePriority GetMessagePriority { get; private set; } = MessagePriority.Normal;

    public List<string>? GetRecipients { get; private set; }

    public List<string>? GetCarbonCopies { get; private set; }

    public List<string>? GetBlindCarbonCopies { get; private set; }

    public List<MimePart>? GetAttachments { get; private set; }

    /// <summary>
    /// From address
    /// </summary>
    /// <param name="from"></param>
    /// <returns></returns>
    public Mail From(string from)
    {
        GetFrom = from;
        return this;
    }

    /// <summary>
    /// Recipients
    /// </summary>
    /// <param name="recipients"></param>
    /// <returns></returns>
    public Mail To(IList<string> recipients)
    {
        if (recipients.IsNullOrEmpty()) return this;
        GetRecipients ??= new List<string>();
        GetRecipients.AddRange(recipients);
        return this;
    }

    /// <summary>
    /// Recipients
    /// </summary>
    /// <param name="recipients"></param>
    /// <returns></returns>
    public Mail To(params string[] recipients)
    {
        if (recipients.IsNullOrEmpty()) return this;
        GetRecipients ??= new List<string>();
        GetRecipients.AddRange(recipients);
        return this;
    }

    /// <summary>
    /// Carbon copy
    /// </summary>
    /// <param name="carbonCopies"></param>
    /// <returns></returns>
    public Mail Cc(IList<string> carbonCopies)
    {
        if (carbonCopies.IsNullOrEmpty()) return this;
        GetCarbonCopies ??= new List<string>();
        GetCarbonCopies.AddRange(carbonCopies);
        return this;
    }

    /// <summary>
    /// Carbon copy
    /// </summary>
    /// <param name="carbonCopies"></param>
    /// <returns></returns>
    public Mail Cc(params string[] carbonCopies)
    {
        if (carbonCopies.IsNullOrEmpty()) return this;
        GetCarbonCopies ??= new List<string>();
        GetCarbonCopies.AddRange(carbonCopies);
        return this;
    }

    /// <summary>
    /// Blind carbon copy
    /// </summary>
    /// <param name="blindCarbonCopies"></param>
    /// <returns></returns>
    public Mail Bcc(IList<string> blindCarbonCopies)
    {
        if (blindCarbonCopies.IsNullOrEmpty()) return this;
        GetBlindCarbonCopies ??= new List<string>();
        GetBlindCarbonCopies.AddRange(blindCarbonCopies);
        return this;
    }

    /// <summary>
    /// Blind carbon copy
    /// </summary>
    /// <param name="blindCarbonCopies"></param>
    /// <returns></returns>
    public Mail Bcc(params string[] blindCarbonCopies)
    {
        if (blindCarbonCopies.IsNullOrEmpty()) return this;
        GetBlindCarbonCopies ??= new List<string>();
        GetBlindCarbonCopies.AddRange(blindCarbonCopies);
        return this;
    }

    public Mail Attachment(Stream stream, string fileName)
    {
        if (stream is null) return this;
        GetAttachments ??= new List<MimePart>();
        var attachment = new MimePart
        {
            Content = new MimeContent(stream),
            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            ContentTransferEncoding = ContentEncoding.Base64,
            FileName = fileName
        };
        GetAttachments.Add(attachment);
        return this;
    }

    public Mail Subject(string subject)
    {
        GetSubject = subject;
        return this;
    }

    public Mail Body(string body)
    {
        GetBody = body;
        return this;
    }

    public Mail Priority(MessagePriority messagePriority)
    {
        GetMessagePriority = messagePriority;
        return this;
    }

    public MimeMessage CreateMail()
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(GetFrom, GetFrom));
        if (!GetRecipients.IsNullOrEmpty())
            mail.To.AddRange(GetRecipients.Select(to => new MailboxAddress(to, to)));
        if (!GetCarbonCopies.IsNullOrEmpty())
            mail.Cc.AddRange(GetCarbonCopies.Select(cc => new MailboxAddress(cc, cc)));
        if (!GetBlindCarbonCopies.IsNullOrEmpty())
            mail.Bcc.AddRange(GetBlindCarbonCopies.Select(bcc => new MailboxAddress(bcc, bcc)));

        mail.Subject = GetSubject;
        var multipart = new Multipart { new TextPart("html") { Text = GetBody } };
        GetAttachments?.ForEach(attachment => multipart.Add(attachment));
        mail.Body = multipart;
        mail.Priority = GetMessagePriority;

        return mail;
    }
}