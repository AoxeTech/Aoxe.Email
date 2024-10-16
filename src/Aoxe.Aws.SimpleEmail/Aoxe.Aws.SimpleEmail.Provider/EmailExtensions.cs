﻿namespace Aoxe.Aws.SimpleEmail.Provider;

public static class EmailExtensions
{
    public static SendEmailRequest ToSendEmailRequest(this Email.Abstractions.Models.Email email)
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
                Raw = new RawMessage { Data = CreateMessageStream(email) }
            }
        };
        return sendEmailRequest;
    }

    private static MemoryStream CreateMessageStream(Email.Abstractions.Models.Email email)
    {
        // Create the body of the email
        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = email.Content.HtmlBody,
            TextBody = email.Content.TextBody
        };

        // Add attachments
        email.Attachments.ForEach(attachment =>
        {
            var mediaTypes = attachment.ContentType.Split('/');
            var mediaType = mediaTypes.FirstOrDefault();
            var mediaSubType = mediaTypes.LastOrDefault();
            bodyBuilder.Attachments.Add(
                attachment.Name,
                attachment.Content,
                new ContentType(
                    mediaType ?? Defaults.MediaTypeApplication,
                    mediaSubType ?? Defaults.MediaSubTypeOctetStream
                )
            );
        });

        var stream = new MemoryStream();
        new MimeMessage
        {
            Subject = email.Content.Subject,
            Body = bodyBuilder.ToMessageBody()
        }.WriteTo(stream);
        return stream;
    }
}
