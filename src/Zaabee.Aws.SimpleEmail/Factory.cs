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
                Simple = new Message
                {
                    Subject = new Content { Data = email.Content.Subject },
                    Body = new Body
                    {
                        Html = new Content { Data = email.Content.HtmlBody },
                        Text = new Content { Data = email.Content.TextBody }
                    }
                },
                // Raw = new RawMessage
                // {
                //     Data = new MemoryStream()
                // },
                // Template = new Template
                // {
                //     TemplateArn = string.Empty,
                //     TemplateData = string.Empty,
                //     TemplateName = string.Empty
                // }
            }
        };
        return sendEmailRequest;
    }
}
