namespace Aoxe.SmtpClient;

public class SmtpProvider(System.Net.Mail.SmtpClient smtpClient) : IEmailProvider
{
    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    ) => await smtpClient.SendMailAsync(Factory.Create(emailCommand));

    public void Dispose()
    {
        smtpClient.Dispose();
    }
}
