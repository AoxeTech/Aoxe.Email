namespace Zaabee.Email.TestProject;

public class EmailCommandTest
{
    [Fact]
    public void AwsEmailProviderTest()
    {
        using var awsEmailProvider = new AwsSimpleEmailProvider(
            new AmazonSimpleEmailServiceV2Client()
        );
        EmailTest(awsEmailProvider);
    }

    [Fact]
    public void AzureEmailProviderTest()
    {
        using var azureEmailProvider = new AzureEmailProvider(new EmailClient(""));
        EmailTest(azureEmailProvider);
    }

    [Fact]
    public void MailKitProviderTest()
    {
        using var client = new global::MailKit.Net.Smtp.SmtpClient();
        using var mailKitProvider = new MailKitProvider(client);
        EmailTest(mailKitProvider);
    }

    [Fact]
    public void SmtpClientProviderTest()
    {
        using var smtpClientProvider = new SmtpProvider(
            new System.Net.Mail.SmtpClient("192.168.78.130", 2525)
        );
        EmailTest(smtpClientProvider);
    }

    private void EmailTest(IEmailProvider emailProvider)
    {
        Assert.Null(emailProvider.Email);
        emailProvider.Email = new Abstractions.Models.Email();
        Assert.NotNull(emailProvider.Email);
        emailProvider.CleanEmail();
        Assert.Null(emailProvider.Email);
    }
}
