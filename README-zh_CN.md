# Aoxe.Email

[English](README.md) | 简体中文

电子邮件作为数字时代的信息使者，已经超越了时间和空间的限制，以其独特而深远的影响力，悄然融入了人类文明的血脉。

为了实现云中立，我们可能需要一款能帮助我们更高效地管理电子邮件的工具。Aoxe.Email就是这样一款工具。它是一款功能强大的电子邮件客户端，可以帮助您发送电子邮件，并避免与云绑定。

---

## 1. Aoxe.Email 是什么?

Aoxe.Email 有四种发送邮件的实现:

### 1.1. Aoxe.Aws.SimpleEmail

这是 Amazon SES API 第 2 版的首次发布。您可以使用此 API 配置您的 Amazon SES 账户并发送电子邮件。此 API 扩展了前一版本 Amazon SES API 中的功能.

### 1.2. Aoxe.Azure.Email

通过该客户端库，可以使用 Microsoft Azure Communication Email 服务。

### 1.3. Aoxe.MailKit

MailKit 是一个开源跨平台 .NET 邮件客户端库，它基于 MimeKit 并针对移动设备进行了优化。

### 1.4. Aoxe.SmtpClient

使用了 System.Net.Mail.SmtpClient.

## 2. 如何使用 Aoxe.Email?

Install the package

```bash
PM> Install-Package Aoxe.Aws.SimpleEmail
PM> Install-Package Aoxe.Azure.Email
PM> Install-Package Aoxe.MailKit
PM> Install-Package Aoxe.SmtpClient
```

注册 email provider. 我们可以很容易地切换不同的实现, 因为所有实现都基于同样的抽象.

```csharp
// 请使用您自己的 host 和 端口, 当然您还可以使用账号密码等功能
serviceCollection.AddMailKit("your host", 25);
serviceCollection.AddSmtpClient("your host", 25);
// AWS 和 Azure 的 Add 方法还有不同的方法重载, 以匹配它们的构造函数以适应不同的场景
serviceCollection.AddAwsSimpleEmail("awsAccessKeyId", "awsSecretAccessKey");
serviceCollection.AddAzureEmail("connectionString");
```

通过依赖 Aoxe.Email.Abstractions 注入 email provider.

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
        // 您可以使用扩展方法来发送邮件
        await email.SendByAsync(_emailProvider);
        // 也可以使用 provider 发送邮件
        await _emailProvider.SendAsync(email);
    }
}
```

## 3. 关于 Aoxe.Email.Abstractions.Models.Email

Aoxe.Email.Abstractions.Models.Email 类是电子邮件的综合表示法。下面简要介绍一下它的结构和功能：

1. 它使用主构造函数来选择性地设置 ID。
2. 它具有各种电子邮件组件的属性，如 From、Sender、ReplyTo、Recipients、Content 和 Attachments。
3. 该类实现了流畅的接口模式，允许使用方法链来轻松组成电子邮件。

主要功能:

1. 附件处理（单个和多个）
2. 设置电子邮件主题、正文和 HTML 正文
3. 管理电子邮件地址（发件人、收件人、抄送人、密送人、回复人、发件人）
4. 每个方法都返回此值，从而实现方法链风格。

您可以这样实例化一个 Email 对象:

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

还可以使用 fluent style:

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

在每个实现包中，Aoxe.Email.Abstractions.Models.Email 都有其扩展方法，可将其转换为电子邮件客户端所需的指定电子邮件模型：

```csharp
// 返回 Amazon.SimpleEmailV2.Model.SendEmailRequest, 可用于 IAmazonSimpleEmailServiceV2
email.ToSendEmailRequest();
// 返回 Azure.Communication.Email.EmailMessage, 可用于 Azure.Communication.Email.EmailClient
email.ToEmailMessage();
// 返回 MimeKit.MimeMessage, 可用于 IMailTransport
email.ToMimeMessage();
// 返回 System.Net.Mail.MailMessage, 可用于 System.Net.Mail.SmtpClient
email.ToMailMessage();
```

这将帮助您在使用电子邮件客户端时进行更多定制，以下是 Aoxe.MailKit 中的示例：

```csharp
var email = new Models.Email();
var mimeMessage = email.ToMimeMessage();
// 使用 memory stream 作为示例
var smtpClient = new SmtpClient(new ProtocolLogger(new MemoryStream()));
await smtpClient.ConnectAsync(host, port);
// 如果需要认证
await smtpClient.AuthenticateAsync(userName, password);
await _mailTransport.SendAsync(mimeMessage, cancellationToken);
```

Thank`s for [JetBrains](https://www.jetbrains.com/) for the great support in providing assistance and user-friendly environment for my open source projects.

[![JetBrains](https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg?_gl=1*f25lxa*_ga*MzI3ODk2MjY0LjE2NzA0NjY4MDQ.*_ga_9J976DJZ68*MTY4OTY4NzY5OS4zNC4xLjE2ODk2ODgwMDAuNTMuMC4w)](https://www.jetbrains.com/community/opensource/#support)
