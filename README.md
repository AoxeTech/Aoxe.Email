# Aoxe.Email

Email, as a messenger of information in the digital age, has transcended the limitations of time and space and, with its unique and far-reaching influence, has been quietly integrated into the veins of civilisation.

For cloud neutrality, we may need a tool that can help us manage our email more efficiently. Aoxe.Email is such a tool. It is a powerful email client that can help you send emails, and avoids being tied to the cloud.

---

## 1. What is Aoxe.Email?

The Aoxe.Email has four implements for sending emails:

### 1.1. Aoxe.Aws.SimpleEmail

This is the first release of version 2 of the Amazon SES API. You can use this API to configure your Amazon SES account, and to send email. This API extends the functionality that exists in the previous version of the Amazon SES API.

### 1.2. Aoxe.Azure.Email

This client library enables working with the Microsoft Azure Communication Email service.

### 1.3. Aoxe.MailKit

MailKit is an Open Source cross-platform .NET mail-client library that is based on MimeKit and optimized for mobile devices.

### 1.4. Aoxe.SmtpClient

Use System.Net.Mail.SmtpClient.

## 2. How to use Aoxe.Email?

Install the package

```bash
PM> Install-Package Aoxe.Aws.SimpleEmail
PM> Install-Package Aoxe.Azure.Email
PM> Install-Package Aoxe.MailKit
PM> Install-Package Aoxe.SmtpClient
```

Register the email provider. All the implements has the same abstractions so we can easily switch between them.

```csharp
// Please replace the host and port with your own
serviceCollection.AddMailKit("your host", 25);
serviceCollection.AddSmtpClient("your host", 25);
// The aws and azure email provider has different Add method to match their own constructors
serviceCollection.AddAwsSimpleEmail("awsAccessKeyId", "awsSecretAccessKey");
serviceCollection.AddAzureEmail("connectionString");
```

Inject the email provider by reference Aoxe.Email.Abstractions.

```shell
PM> Install-Package Aoxe.Email.Abstractions
```

```csharp
public class EmailService
{
    private readonly IEmailProvider _emailProvider;

    public EmailService(IEmailProvider emailProvider)
    {
        _emailProvider = emailProvider;
    }

    public async Task SendEmailAsync(string subject, string text, string fromAddress, string toAddress)
    {
        var email = new Abstractions.Models.Email();
        email
            .Subject(subject)
            .TextBody(email)
            .EmailFrom(fromAddress)
            .EmailTo(toAddress);
        // You can send the email with extension method
        await email.SendByAsync(_emailProvider);
        // Or you can send the email with the provider
        await _emailProvider.SendAsync(email);
    }
}
```

## 3. About Aoxe.Email.Abstractions.Models.Email

The Aoxe.Email.Abstractions.Models.Email class is a comprehensive representation of an email message. Here's a brief overview of its structure and functionality:

1. It uses the primary constructor to optionally set an ID.
2. It has properties for various email components like From, Sender, ReplyTo, Recipients, Content, and Attachments.
3. The class implements a fluent interface pattern, allowing method chaining for easy email composition.

Key features:

1. Attachment handling (single and multiple)
2. Setting email subject, text body, and HTML body
3. Managing email addresses (From, To, Cc, Bcc, ReplyTo, Sender)
4. Each method returns this, enabling method chaining.

You can new an Email like this:

```csharp
var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
_ = new Models.Email
{
    From = new EmailAddress("from@fake.com", "from"),
    Sender = new EmailAddress("sender@fake.com", "sender"),
    Recipients = new EmailRecipients
    {
        To = [new EmailAddress("to@fake.com", "to")],
        Cc = [new EmailAddress("cc@fake.com", "cc")],
        Bcc = [new EmailAddress("bcc@fake.com", "bcc")],
    },
    ReplyTo = [new EmailAddress("replyto@fake.com", "replyTo")],
    Content = new EmailContent
    {
        Subject = "Subject",
        TextBody = "TextBody",
        HtmlBody = "HtmlBody",
    },
    Attachments = [new EmailAttachment(fileName, fileBytes)]
};
```

Also you can use a fluent style:

```csharp
var email = new Models.Email();
email
    .Subject("subject")
    .TextBody("textBody")
    .HtmlBody("htmlBody")
    .EmailFrom("address", "name")
    .EmailTo("address0", "name0")
    .EmailTo([new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")])
    .EmailCc("address0", "name0")
    .EmailCc([new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")])
    .EmailBcc("address0", "name0")
    .EmailBcc(
        [new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")]
    )
    .EmailReplyTo("address0", "name0")
    .EmailReplyTo(
        [new EmailAddress("address1", "name1"), new EmailAddress("address2", "name2")]
    )
    .EmailSender("address", "name");
var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync("AttachmentTestFile.txt");
email
    .Attach(fileName, fileBytes)
    .Attach([new(fileName, fileBytes), new(fileName, fileBytes)]);
```

In each implement package the Aoxe.Email.Abstractions.Models.Email has its extension methord to convert it into the specify email model which the email client want:

```csharp
// Return Amazon.SimpleEmailV2.Model.SendEmailRequest for IAmazonSimpleEmailServiceV2
email.ToSendEmailRequest();
// Return Azure.Communication.Email.EmailMessage for Azure.Communication.Email.EmailClient
email.ToEmailMessage();
// Return MimeKit.MimeMessage for IMailTransport
email.ToMimeMessage();
// Return System.Net.Mail.MailMessage for System.Net.Mail.SmtpClient
email.ToMailMessage();
```

This will help you to use the email client with more customization, here is the example in Aoxe.MailKit:

```csharp
var email = new Models.Email();
var mimeMessage = email.ToMimeMessage();
// Just a demo so use memory stream.
var smtpClient = new SmtpClient(new ProtocolLogger(new MemoryStream()));
await smtpClient.ConnectAsync(host, port);
// If need authenticate
await smtpClient.AuthenticateAsync(userName, password);
await _mailTransport.SendAsync(mimeMessage, cancellationToken);
```

Thank`s for [JetBrains](https://www.jetbrains.com/) for the great support in providing assistance and user-friendly environment for my open source projects.

[![JetBrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg?_gl=1*f25lxa*_ga*MzI3ODk2MjY0LjE2NzA0NjY4MDQ.*_ga_9J976DJZ68*MTY4OTY4NzY5OS4zNC4xLjE2ODk2ODgwMDAuNTMuMC4w)](https://www.jetbrains.com/community/opensource/#support)
