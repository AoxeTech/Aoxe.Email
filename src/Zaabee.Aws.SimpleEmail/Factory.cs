namespace Zaabee.Aws.SimpleEmail;

public static class Factory
{
    public static SendEmailRequest Create(SendEmailCommand emailCommand)
    {
        var email = new SendEmailRequest
        {
            FromEmailAddress = emailCommand.From.Address,
            Destination = new Destination
            {
                ToAddresses = emailCommand.Recipients.To.Select(to => to.Address).ToList(),
                CcAddresses = emailCommand.Recipients.Cc.Select(cc => cc.Address).ToList(),
                BccAddresses = emailCommand.Recipients.Bcc.Select(bcc => bcc.Address).ToList()
            },
            Content = new Amazon.SimpleEmailV2.Model.EmailContent
            {
                Simple = new Message
                {
                    Subject = new Content
                    {
                        Data = emailCommand.Content.Subject
                    },
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Data = emailCommand.Content.Html
                        },
                        Text = new Content
                        {
                            Data = emailCommand.Content.PlainText
                        }
                    }
                }
            }
        };

        return email;
    }
}