namespace Zaabee.SmtpClient;

public static class Factory
{
    public static MailMessage Create(SendEmailCommand emailCommand)
    {
        var email = new MailMessage
        {
            From = new MailAddress(emailCommand.From.Address, emailCommand.From.Name),
            Sender = new MailAddress(emailCommand.Sender.Address, emailCommand.Sender.Name),
            Subject = emailCommand.Content.Subject,
            IsBodyHtml = !emailCommand.Content.Html.IsNullOrEmpty(),
            BodyEncoding = Encoding.UTF8,
            Body = emailCommand.Content.Html.IsNullOrEmpty() ? emailCommand.Content.PlainText : emailCommand.Content.Html,
            Priority = MailPriority.Normal,
            SubjectEncoding = Encoding.UTF8,
            HeadersEncoding = Encoding.UTF8
        };

        emailCommand.Recipients.To.ForEach(to => email.To.Add(new MailAddress(to.Address, to.Name)));
        emailCommand.Recipients.Cc.ForEach(cc => email.CC.Add(new MailAddress(cc.Address, cc.Name)));
        emailCommand.Recipients.Bcc.ForEach(bcc => email.Bcc.Add(new MailAddress(bcc.Address, bcc.Name)));
        emailCommand.ReplyTo.ForEach(replyTo => email.ReplyToList.Add(new MailAddress(replyTo.Address, replyTo.Name)));

        return email;
    }
}