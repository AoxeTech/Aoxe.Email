namespace Aoxe.Email.TestProject;

public class EmailProviderTest
{
    [Fact]
    public async Task AwsEmailProviderTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAwsSimpleEmail();
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;
        using var azureEmailProvider = provider.GetRequiredService<IEmailProvider>();
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task AzureEmailProviderTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddAzureEmail(string.Empty);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;
        using var azureEmailProvider = provider.GetRequiredService<IEmailProvider>();
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task MailKitProviderTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMailKit("192.168.78.130", 2525);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;
        using var azureEmailProvider = provider.GetRequiredService<IEmailProvider>();
        await SendEmailAsync(azureEmailProvider);
    }

    [Fact]
    public async Task SmtpClientProviderTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSmtpClient("192.168.78.130", 2525);
        var serviceProvider = serviceCollection.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;
        using var azureEmailProvider = provider.GetRequiredService<IEmailProvider>();
        await SendEmailAsync(azureEmailProvider);
    }

    private static async Task SendEmailAsync(IEmailProvider emailProvider)
    {
        var (fileName, fileBytes) = await FileHelper.LoadFileBytesAsync(
            ".\\AttachmentTestFile.txt"
        );
        var email = new Abstractions.Models.Email();
        email
            .Subject("subject")
            .TextBody("textBody")
            .HtmlBody("htmlBody")
            .EmailFrom("fromAddress@mock.com", "fromName")
            .EmailTo("toAddress0@mock.com", "name0")
            .EmailTo(
                [
                    new EmailAddress("toAddress1@mock.com", "name1"),
                    new EmailAddress("toAddress2@mock.com", "name2")
                ]
            )
            .EmailCc("ccAddress0@mock.com", "name0")
            .EmailCc(
                [
                    new EmailAddress("ccAddress1@mock.com", "name1"),
                    new EmailAddress("ccAddress2@mock.com", "name2")
                ]
            )
            .EmailBcc("bccAddress0@mock.com", "name0")
            .EmailBcc(
                [
                    new EmailAddress("bccAddress1@mock.com", "name1"),
                    new EmailAddress("bccAddress2@mock.com", "name2")
                ]
            )
            .EmailReplyTo("replyToAddress0@mock.com", "name0")
            .EmailReplyTo(
                [
                    new EmailAddress("replyToAddress1@mock.com", "name1"),
                    new EmailAddress("replyToAddress2@mock.com", "name2")
                ]
            )
            .EmailSender("senderAddress@mock.com", "name")
            .Attach(fileName, fileBytes)
            .Attach([new(fileName, fileBytes), new(fileName, fileBytes)]);
        await emailProvider.SendAsync(email);
    }
}
