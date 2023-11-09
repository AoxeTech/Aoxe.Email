using System;

namespace Zaabee.MailKit;

public static class Factory
{
    public static MimeMessage Create(EmailCommand emailCommand)
    {
        var email = new MimeMessage
        {
            From = { new MailboxAddress(emailCommand.From.Name, emailCommand.From.Address) },
            Sender = new MailboxAddress(emailCommand.Sender.Name, emailCommand.Sender.Address),
            Subject = emailCommand.Content.Subject,
            Body = new Multipart
            {
                new TextPart(TextFormat.Html) { Text = emailCommand.Content.Html },
                new TextPart(TextFormat.Plain) { Text = emailCommand.Content.PlainText }
            },
            Priority = MessagePriority.Normal,
            Importance = MessageImportance.Normal,
            MessageId = emailCommand.Id,
            Date = DateTimeOffset.Now
        };
        email.To.AddRange(emailCommand.Recipients.To.Select(to => new MailboxAddress(to.Name, to.Address)));
        email.Cc.AddRange(emailCommand.Recipients.Cc.Select(cc => new MailboxAddress(cc.Name, cc.Address)));
        email.Bcc.AddRange(emailCommand.Recipients.Bcc.Select(bcc => new MailboxAddress(bcc.Name, bcc.Address)));
        email.ReplyTo.AddRange(emailCommand.ReplyTo.Select(replyTo => new MailboxAddress(replyTo.Name, replyTo.Address)));

        return email;
    }
}