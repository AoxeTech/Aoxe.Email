namespace Zaabee.Azure.Email;

public class AzureEmailProvider(EmailClient emailClient) : IEmailProvider
{
    public async ValueTask SendAsync(Zaabee.Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await emailClient.SendAsync(WaitUntil.Completed, Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {

    }
}