namespace Aoxe.Azure.Email;

public static class EmailExtensions
{
    public static EmailMessage ToEmailMessage(this Aoxe.Email.Abstractions.Models.Email email)
    {
        var emailMessage = new EmailMessage(
            email.From.Address,
            new EmailRecipients(
                email.Recipients.To.Select(to => new EmailAddress(to.Address, to.Name)),
                email.Recipients.Cc.Select(cc => new EmailAddress(cc.Address, cc.Name)),
                email.Recipients.Bcc.Select(bcc => new EmailAddress(bcc.Address, bcc.Name))
            ),
            new EmailContent(email.Content.Subject)
            {
                Html = email.Content.HtmlBody,
                PlainText = email.Content.TextBody
            }
        );
        emailMessage.ReplyTo.AddRange(
            email.ReplyTo.Select(replyTo => new EmailAddress(replyTo.Address, replyTo.Name))
        );
        emailMessage.Attachments.AddRange(
            email.Attachments.Select(attachment => new EmailAttachment(
                attachment.Name,
                attachment.ContentType,
                BinaryData.FromBytes(attachment.Content)
            ))
        );

        return emailMessage;
    }
}
