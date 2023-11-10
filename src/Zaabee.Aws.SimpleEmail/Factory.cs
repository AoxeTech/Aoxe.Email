﻿namespace Zaabee.Aws.SimpleEmail;

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
                    Subject = new Content
                    {
                        Data = email.Content.Subject
                    },
                    Body = new Body
                    {
                        Html = new Content
                        {
                            Data = email.Content.Html
                        },
                        Text = new Content
                        {
                            Data = email.Content.PlainText
                        }
                    }
                },
                Raw = new RawMessage
                {
                    
                },
                Template = new Template
                {
                    TemplateArn = string.Empty,
                    TemplateData = string.Empty,
                    TemplateName = string.Empty
                }
            }
        };

        var i = new Message();
        var j = new RawMessage();

        return sendEmailRequest;
    }
}