namespace Aoxe.SmtpClient.Provider;

public sealed class SmtpProvider(ISmtpClientFactory smtpClientFactory) : IEmailProvider
{
    private System.Net.Mail.SmtpClient? _smtpClient;

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    )
    {
        _smtpClient ??= smtpClientFactory.Create();
#if NETSTANDARD2_0
        await _smtpClient.SendMailAsync(emailCommand.ToMailMessage());
#else
        await _smtpClient.SendMailAsync(emailCommand.ToMailMessage(), cancellationToken);
#endif
    }

    public void Dispose() => _smtpClient?.Dispose();
}
