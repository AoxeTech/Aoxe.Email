using Zaabee.Email.Abstractions.Models;

namespace Zaabee.MailKit;

public class MailKitProvider : IEmailProvider
{
    private readonly SmtpClient _smtpClient;

    public MailKitProvider(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async ValueTask SendAsync(Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await _smtpClient.SendAsync(Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {
        _smtpClient.Dispose();
    }
}