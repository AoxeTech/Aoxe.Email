namespace Zaabee.SmtpClient;

public class SmtpProvider(System.Net.Mail.SmtpClient smtpClient) : IEmailProvider
{
    public Email.Abstractions.Models.Email? Email { get; set; }

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email emailCommand,
        CancellationToken cancellationToken = default
    ) => await smtpClient.SendMailAsync(Factory.Create(emailCommand));

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose()
    {
        smtpClient.Dispose();
    }
}
