namespace Aoxe.SmtpClient;

public class SmtpProvider(
    string host,
    int port = 2525,
    string? userName = null,
    string? password = null,
    bool? enableSsl = null
) : IEmailProvider
{
    private readonly System.Net.Mail.SmtpClient _smtpClient = new(host, port);

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    )
    {
        if (userName is not null && password is not null)
            _smtpClient.Credentials = new NetworkCredential(userName, password);
        if (enableSsl.HasValue)
            _smtpClient.EnableSsl = enableSsl.Value;
        await _smtpClient.SendMailAsync(Factory.Create(emailCommand));
    }

    public void Dispose() => _smtpClient.Dispose();
}
