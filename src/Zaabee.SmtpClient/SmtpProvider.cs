using Zaabee.Email.Abstractions.Models;

namespace Zaabee.SmtpClient;

public class SmtpProvider : IEmailProvider
{
    private readonly System.Net.Mail.SmtpClient _smtpClient;

    public SmtpProvider(System.Net.Mail.SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async ValueTask SendAsync(Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await _smtpClient.SendMailAsync(Factory.Create(emailCommand));

    public void Dispose()
    {
        // Transient
        _smtpClient.Dispose();
    }
}