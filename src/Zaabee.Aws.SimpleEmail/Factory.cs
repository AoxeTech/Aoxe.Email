namespace Zaabee.Aws.SimpleEmail;

public static class Factory
{
    public static SendEmailRequest Create(Email.Abstractions.Models.Email email)
    {
        var sendEmailRequest = new SendEmailRequest
        {
            FromEmailAddress = email.From.Address,
            Destination = new Destination
            {
                ToAddresses = email.Recipients.To.Select(to => to.Address).ToList(),
                CcAddresses = email.Recipients.Cc.Select(cc => cc.Address).ToList(),
                BccAddresses = email.Recipients.Bcc.Select(bcc => bcc.Address).ToList()
            },
            Content = new EmailContent
            {
                Raw = new RawMessage
                {
                    Data = CreateMessageStream(email.Content.Subject, email.Content.HtmlBody, email.Content.TextBody)
                }
            }
        };
        return sendEmailRequest;
    }

    private static MemoryStream CreateMessageStream(string subject, string html, string text)
    {
        var stream = new MemoryStream();
        new MimeMessage
        {
            Subject = subject,
            Body = new BodyBuilder
            {
                HtmlBody = html,
                TextBody = text
            }.ToMessageBody()
        }.WriteTo(stream);
        return stream;
    }
}
