namespace Zaabee.MailKit;

public static class Factory
{
    public static MimeMessage Create(Email.Abstractions.Models.Email email)
    {
        var mimeMessage = new MimeMessage
        {
            From = { new MailboxAddress(email.From.Name, email.From.Address) },
            Sender = string.IsNullOrWhiteSpace(email.Sender.Address)
                ? new MailboxAddress(email.From.Name, email.From.Address)
                : new MailboxAddress(email.Sender.Name, email.Sender.Address),
            Subject = email.Content.Subject,
            Body = new Multipart
            {
                new TextPart(TextFormat.Html) { Text = email.Content.HtmlBody },
                new TextPart(TextFormat.Plain) { Text = email.Content.TextBody }
            },
            Priority = MessagePriority.Normal,
            Importance = MessageImportance.Normal,
            MessageId = email.Id,
            Date = DateTimeOffset.Now
        };

        var body = new Multipart
        {
            new TextPart(TextFormat.Html) { Text = email.Content.HtmlBody },
            new TextPart(TextFormat.Plain) { Text = email.Content.TextBody }
        };
        body.AddRange(
            email
                .Attachments
                .Select(attachment =>
                {
                    var mediaTypes = attachment.ContentType.Split('/');
                    var mediaType = mediaTypes.FirstOrDefault();
                    var mediaSubType = mediaTypes.LastOrDefault();
                    return new MimePart(
                        new ContentType(
                            mediaType ?? MediaType.Application,
                            mediaSubType ?? MediaSubType.OctetStream
                        )
                    )
                    {
                        Content = new MimeContent(attachment.Content.ToMemoryStream()),
                        ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                        ContentTransferEncoding = ContentEncoding.Base64,
                        FileName = attachment.Name
                    };
                })
        );
        mimeMessage.Body = body;

        mimeMessage
            .To
            .AddRange(email.Recipients.To.Select(to => new MailboxAddress(to.Name, to.Address)));
        mimeMessage
            .Cc
            .AddRange(email.Recipients.Cc.Select(cc => new MailboxAddress(cc.Name, cc.Address)));
        mimeMessage
            .Bcc
            .AddRange(
                email.Recipients.Bcc.Select(bcc => new MailboxAddress(bcc.Name, bcc.Address))
            );
        mimeMessage
            .ReplyTo
            .AddRange(
                email.ReplyTo.Select(replyTo => new MailboxAddress(replyTo.Name, replyTo.Address))
            );

        return mimeMessage;
    }
}
