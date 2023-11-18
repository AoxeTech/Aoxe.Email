namespace Zaabee.SmtpClient;

public static class Factory
{
    public static MailMessage Create(Email.Abstractions.Models.Email email)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(email.From.Address, email.From.Name),
            Sender = string.IsNullOrWhiteSpace(email.Sender?.Address)
                ? new MailAddress(email.From.Address, email.From.Name)
                : new MailAddress(email.Sender!.Address, email.Sender.Name),
            Subject = email.Content.Subject,
            IsBodyHtml = !string.IsNullOrWhiteSpace(email.Content.Html),
            BodyEncoding = Encoding.UTF8,
            Body = string.IsNullOrWhiteSpace(email.Content.Html) ? email.Content.PlainText : email.Content.Html,
            Priority = MailPriority.Normal,
            SubjectEncoding = Encoding.UTF8,
            HeadersEncoding = Encoding.UTF8
        };

        email.Recipients.To.ForEach(to => mailMessage.To.Add(new MailAddress(to.Address, to.Name)));
        email.Recipients.Cc.ForEach(cc => mailMessage.CC.Add(new MailAddress(cc.Address, cc.Name)));
        email.Recipients.Bcc.ForEach(bcc => mailMessage.Bcc.Add(new MailAddress(bcc.Address, bcc.Name)));
        email.ReplyTo.ForEach(replyTo => mailMessage.ReplyToList.Add(new MailAddress(replyTo.Address, replyTo.Name)));

        mailMessage.Attachments.AddRange(email.Attachments.Select(attachment =>
            new Attachment(new MemoryStream(attachment.Content), attachment.Name, attachment.ContentType)));
        return mailMessage;
    }
}