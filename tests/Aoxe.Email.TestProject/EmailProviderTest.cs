namespace Aoxe.Email.TestProject;

public class EmailProviderTest
{
    [Fact]
    public async Task AwsEmailProviderTest()
    {
        using var awsEmailProvider = new AwsSimpleEmailProvider();
        await SendEmailAsync(awsEmailProvider);
    }

    [Fact]
    public async Task AzureEmailProviderTest()
    {
        using var azureEmailProvider = new AzureEmailProvider(string.Empty);
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task MailKitProviderTest()
    {
        using var mailKitProvider = new MailKitProvider("192.168.78.130");
        await SendEmailAsync(mailKitProvider);
    }

    [Fact]
    public async Task SmtpClientProviderTest()
    {
        using var smtpClientProvider = new SmtpProvider("192.168.78.130");
        await SendEmailAsync(smtpClientProvider);
    }

    private async Task SendEmailAsync(IEmailProvider emailProvider)
    {
        var (fileName, fileBytes) = await new FileHelper().LoadFileBytesAsync(
            ".\\AttachmentTestFile.txt"
        );
        var email = new Abstractions.Models.Email();
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
            .EmailSender("address", "name")
            .Attach(fileName, fileBytes)
            .Attach([new(fileName, fileBytes), new(fileName, fileBytes)]);
        await emailProvider.SendAsync(email);
    }
}
