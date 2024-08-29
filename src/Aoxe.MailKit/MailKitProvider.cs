namespace Aoxe.MailKit;

public class MailKitProvider(
    string host,
    int port = 2525,
    string? userName = null,
    string? password = null
) : IEmailProvider
{
    private readonly IMailTransport _mailTransport = new SmtpClient();

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    )
    {
        await _mailTransport.ConnectAsync(host, port, cancellationToken: cancellationToken);
        if (userName is not null && password is not null)
            await _mailTransport.AuthenticateAsync(userName, password, cancellationToken);
        await _mailTransport.SendAsync(Factory.Create(emailCommand), cancellationToken);
    }

    public void Dispose() => _mailTransport.Dispose();
}
