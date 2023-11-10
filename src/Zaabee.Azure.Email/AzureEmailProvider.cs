namespace Zaabee.Azure.Email;

public class AzureEmailProvider : IEmailProvider
{
    private readonly EmailClient _emailClient;

    public AzureEmailProvider(EmailClient emailClient)
    {
        _emailClient = emailClient;
    }

    public async ValueTask SendAsync(Zaabee.Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await _emailClient.SendAsync(WaitUntil.Completed, Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {

    }
}