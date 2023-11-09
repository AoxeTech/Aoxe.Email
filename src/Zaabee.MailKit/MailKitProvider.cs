namespace Zaabee.MailKit;

public class MailKitProvider : IEmailProvider
{
    private readonly SmtpClient _smtpClient;

    public MailKitProvider(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async ValueTask SendAsync(SendEmailCommand emailCommand, CancellationToken cancellationToken = default) =>
        await _smtpClient.SendAsync(Factory.Create(emailCommand), cancellationToken);
}