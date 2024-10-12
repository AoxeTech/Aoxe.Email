namespace Aoxe.SmtpClient;

public class SmtpProvider(ISmtpClientFactory smtpClientFactory) : IEmailProvider
{
    private System.Net.Mail.SmtpClient? _smtpClient;

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    )
    {
        _smtpClient ??= smtpClientFactory.Create();
        await _smtpClient.SendMailAsync(emailCommand.ToMailMessage());
    }

    public void Dispose() => _smtpClient?.Dispose();
}
