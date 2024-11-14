namespace Aoxe.MailKit.Provider;

public sealed class MailKitProvider(ISmtpClientFactory smtpClientFactory) : IEmailProvider
{
    private IMailTransport? _mailTransport;

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    )
    {
        _mailTransport ??= await smtpClientFactory.CreateAsync();
        await _mailTransport.SendAsync(emailCommand.ToMimeMessage(), cancellationToken);
    }

    public void Dispose() => _mailTransport?.Dispose();
}
