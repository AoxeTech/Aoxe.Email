# Aoxe.Email

Email, as a messenger of information in the digital age, has transcended the limitations of time and space and, with its unique and far-reaching influence, has been quietly integrated into the veins of civilisation.

For cloud neutrality, we may need a tool that can help us manage our email more efficiently. Aoxe.Email is such a tool. It is a powerful email client that can help you send emails, and avoids being tied to the cloud.

---

## 1. What is Aoxe.Email?

The Aoxe.Email has four implements for sending emails:

### 1.1. Aoxe.Aws.SimpleEmail

This is the first release of version 2 of the Amazon SES API. You can use this API to configure your Amazon SES account, and to send email. This API extends the functionality that exists in the previous version of the Amazon SES API.

### 1.2. Aoxe.Azure.Email

Use Azure.Communication.Email.

### 1.3. Aoxe.MailKit

MailKit is an Open Source cross-platform .NET mail-client library that is based on MimeKit and optimized for mobile devices.

### 1.4. Aoxe.SmtpClient

Use System.Net.Mail.SmtpClient.

## 2. How to use Aoxe.Email?

Install the package

```bash
PM> Install-Package Aoxe.MailKit
```

Register the email provider

```csharp
serviceCollection.AddMailKit("192.168.78.130", 2525);
```

Inject the email provider

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
