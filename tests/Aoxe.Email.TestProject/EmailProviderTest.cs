namespace Aoxe.Email.TestProject;

public class EmailProviderTest
{
    [Fact]
    public async Task AwsEmailProviderTest()
    {
        using var awsEmailProvider = new AwsSimpleEmailProvider(
            new AmazonSimpleEmailServiceV2Client()
        );
        await SendEmailAsync(awsEmailProvider);
    }

    [Fact]
    public async Task AzureEmailProviderTest()
    {
        using var azureEmailProvider = new AzureEmailProvider(new EmailClient(""));
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task MailKitProviderTest()
    {
        using var client = new global::MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync("192.168.78.130", 2525);
        using var mailKitProvider = new MailKitProvider(client);
        await SendEmailAsync(mailKitProvider);
    }

    [Fact]
    public async Task SmtpClientProviderTest()
    {
        using var smtpClientProvider = new SmtpProvider(
            new System.Net.Mail.SmtpClient("192.168.78.130", 2525)
        );
        await SendEmailAsync(smtpClientProvider);
    }

    private async Task SendEmailAsync(IEmailProvider emailProvider)
    {
        var emailCommand = new Abstractions.Models.Email
        {
            From = new EmailAddress { Address = "From@Fake.com", Name = "From" },
            Sender = new EmailAddress { Address = "Sender@Fake.com", Name = "Sender" },
            Content = new EmailContent
            {
                Subject = $"Test {emailProvider.GetType()} {DateTime.UtcNow}",
                TextBody = "This is a test email",
                HtmlBody = "<h1>This is a test email</h1>"
            },
            Recipients = new EmailRecipients
            {
                To =  [new EmailAddress("To@Fake.com", "To")],
                Cc =  [new EmailAddress("Cc@Fake.com", "Cc")],
                Bcc =  [new EmailAddress("Bcc@Fake.com", "Bcc")]
            },
            ReplyTo =  [new("ReplyTo@Fake.com", "ReplyTo")],
            Attachments =
            [
                new EmailAttachment
                {
                    Name = "test1.txt",
                    Content = await FileToBytesAsync(".\\AttachmentTestFile.txt")
                },
                new EmailAttachment
                {
                    Name = "test2.txt",
                    Content = await FileToBytesAsync(".\\AttachmentTestFile.txt")
                }
            ]
        };
        await emailProvider.SendAsync(emailCommand);
    }

    private Task<byte[]> FileToBytesAsync(string path)
    {
        byte[] bytes;
        using (
            var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read)
        )
        {
            bytes = new byte[fileStream.Length];
            _ = fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        return Task.FromResult(bytes);
    }
}
