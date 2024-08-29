namespace Aoxe.Azure.Email;

public class AzureEmailProvider : IEmailProvider
{
    private readonly EmailClient _emailClient;

    public AzureEmailProvider(string connectionString, EmailClientOptions? options = null) =>
        _emailClient = options is null
            ? new EmailClient(connectionString)
            : new EmailClient(connectionString, options);

    public AzureEmailProvider(
        Uri endpoint,
        AzureKeyCredential credential,
        EmailClientOptions? options = null
    ) => _emailClient = new EmailClient(endpoint, credential, options);

    public AzureEmailProvider(
        Uri endpoint,
        TokenCredential credential,
        EmailClientOptions? options = null
    ) => _emailClient = new EmailClient(endpoint, credential, options);

    public async ValueTask SendAsync(
        Aoxe.Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    ) =>
        await _emailClient.SendAsync(WaitUntil.Completed, Factory.Create(email), cancellationToken);

    public void Dispose() { }
}
