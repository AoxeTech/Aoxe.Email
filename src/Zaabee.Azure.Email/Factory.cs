using EmailAddress = Azure.Communication.Email.EmailAddress;
using EmailContent = Azure.Communication.Email.EmailContent;
using EmailRecipients = Azure.Communication.Email.EmailRecipients;

namespace Zaabee.Azure.Email;

public static class Factory
{
    public static EmailMessage Create(SendEmailCommand emailCommand)
    {
        var email =
            new EmailMessage(emailCommand.From.Address,
                new EmailRecipients(
                    emailCommand.Recipients.To.Select(to => new EmailAddress(to.Address, to.Name)),
                    emailCommand.Recipients.Cc.Select(cc => new EmailAddress(cc.Address, cc.Name)),
                    emailCommand.Recipients.Bcc.Select(bcc => new EmailAddress(bcc.Address, bcc.Name))),
                new EmailContent(emailCommand.Content.Subject)
                {
                    Html = emailCommand.Content.Html,
                    PlainText = emailCommand.Content.PlainText
                });
        emailCommand.ReplyTo.ForEach(replyTo => email.ReplyTo.Add(new EmailAddress(replyTo.Address, replyTo.Name)));

        return email;
    }
}