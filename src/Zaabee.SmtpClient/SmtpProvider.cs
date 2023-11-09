namespace Zaabee.SmtpClient;

public class SmtpProvider : IEmailProvider
{
    private readonly System.Net.Mail.SmtpClient _smtpClient;

    public SmtpProvider(System.Net.Mail.SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async ValueTask SendAsync(EmailCommand emailCommand, CancellationToken cancellationToken = default) =>
        await _smtpClient.SendMailAsync(Factory.Create(emailCommand));
}