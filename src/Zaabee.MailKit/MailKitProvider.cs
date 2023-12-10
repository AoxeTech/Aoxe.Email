namespace Zaabee.MailKit;

public class MailKitProvider(IMailTransport smtpClient) : IEmailProvider
{
    public async ValueTask SendAsync(Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await smtpClient.SendAsync(Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {
        smtpClient.Dispose();
    }
}