namespace Zaabee.MailKit;

public class MailKitProvider(IMailTransport smtpClient) : IEmailProvider
{
    public Email.Abstractions.Models.Email? Email { get; set; }

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    ) => await smtpClient.SendAsync(Factory.Create(emailCommand), cancellationToken);

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose()
    {
        smtpClient.Dispose();
    }
}
