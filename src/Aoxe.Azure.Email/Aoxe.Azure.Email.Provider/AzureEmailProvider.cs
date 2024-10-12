namespace Aoxe.Azure.Email.Provider;

public class AzureEmailProvider(IEmailClientFactory emailClientFactory) : IEmailProvider
{
    private EmailClient? _emailClient;

    public async ValueTask SendAsync(
        Aoxe.Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    )
    {
        _emailClient ??= emailClientFactory.Create();
        await _emailClient.SendAsync(
            WaitUntil.Completed,
            email.ToEmailMessage(),
            cancellationToken
        );
    }

    public void Dispose() { }
}
