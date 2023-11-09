namespace Zaabee.Azure.Email;

public class AzureEmailProvider : IEmailProvider
{
    private readonly EmailClient _emailClient;

    public AzureEmailProvider(EmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async ValueTask SendAsync(SendEmailCommand emailCommand, CancellationToken cancellationToken = default) =>
        await _emailClient.SendAsync(WaitUntil.Completed, Factory.Create(emailCommand), cancellationToken);
}