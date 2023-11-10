using EmailAddress = Azure.Communication.Email.EmailAddress;
using EmailContent = Azure.Communication.Email.EmailContent;
using EmailRecipients = Azure.Communication.Email.EmailRecipients;

namespace Zaabee.Azure.Email;

public static class Factory
{
    public static EmailMessage Create(Zaabee.Email.Abstractions.Models.Email email)
    {
        var emailMessage =
            new EmailMessage(email.From.Address,
                new EmailRecipients(
                    email.Recipients.To.Select(to => new EmailAddress(to.Address, to.Name)),
                    email.Recipients.Cc.Select(cc => new EmailAddress(cc.Address, cc.Name)),
                    email.Recipients.Bcc.Select(bcc => new EmailAddress(bcc.Address, bcc.Name))),
                new EmailContent(email.Content.Subject)
                {
                    Html = email.Content.Html,
                    PlainText = email.Content.PlainText
                });
        email.ReplyTo.ForEach(replyTo => emailMessage.ReplyTo.Add(new EmailAddress(replyTo.Address, replyTo.Name)));
        email.Attachments.ForEach(attachment =>
            emailMessage.Attachments.Add(
                new EmailAttachment(
                    attachment.Name,
                    attachment.ContentType,
                    BinaryData.FromBytes(attachment.Content))));

        return emailMessage;
    }
}