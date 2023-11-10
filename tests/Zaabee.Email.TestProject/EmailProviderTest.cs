using Amazon.SimpleEmailV2;
using Azure.Communication.Email;
using Zaabee.Aws.SimpleEmail;
using Zaabee.Azure.Email;
using Zaabee.Email.Abstractions;
using Zaabee.MailKit;
using Zaabee.SmtpClient;
using EmailAddress = Zaabee.Email.Abstractions.Models.EmailAddress;
using EmailAttachment = Zaabee.Email.Abstractions.Models.EmailAttachment;
using EmailContent = Zaabee.Email.Abstractions.Models.EmailContent;
using EmailRecipients = Zaabee.Email.Abstractions.Models.EmailRecipients;

namespace Zaabee.Email.TestProject;

public class EmailProviderTest
{
    [Fact]
    public async Task AwsEmailProviderTest()
    {
        var awsEmailProvider = new AwsSimpleEmailProvider(new AmazonSimpleEmailServiceV2Client());
        await SendEmailAsync(awsEmailProvider);
    }

    [Fact]
    public async Task AzureEmailProviderTest()
    {
        var azureEmailProvider = new AzureEmailProvider(new EmailClient(""));
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task MailKitProviderTest()
    {
        var client = new global::MailKit.Net.Smtp.SmtpClient();
        await client.ConnectAsync("192.168.78.130", 2525);
        var mailKitProvider = new MailKitProvider(client);
        await SendEmailAsync(mailKitProvider);
    }

    [Fact]
    public async Task SmtpClientProviderTest()
    {
        var smtpClientProvider = new SmtpProvider(new System.Net.Mail.SmtpClient("192.168.78.130", 2525));
        await SendEmailAsync(smtpClientProvider);
    }

    private async Task SendEmailAsync(IEmailProvider emailProvider)
    {
        var emailCommand = new Abstractions.Models.Email
        {
            From = new EmailAddress
            {
                Address = "From@Fake.com",
                Name = "From"
            },
            Sender = new EmailAddress
            {
                Address = "Sender@Fake.com",
                Name = "Sender"
            },
            Content = new EmailContent
            {
                Subject = $"Test {emailProvider.GetType()} {DateTime.UtcNow}",
                PlainText = "This is a test email",
                Html = "<h1>This is a test email</h1>"
            },
            Recipients = new EmailRecipients
            {
                To = new List<EmailAddress> { new("To@Fake.com", "To") },
                Cc = new List<EmailAddress> { new("Cc@Fake.com", "Cc") },
                Bcc = new List<EmailAddress> { new("Bcc@Fake.com", "Bcc") }
            },
            ReplyTo = new List<EmailAddress> { new("ReplyTo@Fake.com", "ReplyTo") },
            Attachments = new List<EmailAttachment>
            {
                new()
                {
                    Name = "test.txt",
                    Content = (await FileToStreamAsync("D:\\MyProjects\\Zaabee\\Zaabee.Email\\tests\\Zaabee.Email.TestProject\\AttchmentTestFile.txt")).ToArray()
                }
            }
        };
        await emailProvider.SendAsync(emailCommand);
    }

    private Task<MemoryStream> FileToStreamAsync(string path)
    {
        byte[] bytes;
        using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            bytes = new byte[fileStream.Length];
            var read = fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
        }

        return Task.FromResult(new MemoryStream(bytes));
    }
}