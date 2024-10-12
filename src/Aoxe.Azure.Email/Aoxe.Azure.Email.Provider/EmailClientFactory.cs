namespace Aoxe.Azure.Email.Provider;

public class EmailClientFactory : IEmailClientFactory
{
    private readonly Func<EmailClient> _createFunc;

    public EmailClientFactory(string connectionString, EmailClientOptions? options = null) =>
        _createFunc = options is null
            ? () => new EmailClient(connectionString)
            : () => new EmailClient(connectionString, options);

    public EmailClientFactory(
        Uri endpoint,
        AzureKeyCredential credential,
        EmailClientOptions? options = null
    ) => _createFunc = () => new EmailClient(endpoint, credential, options);

    public EmailClientFactory(
        Uri endpoint,
        TokenCredential credential,
        EmailClientOptions? options = null
    ) => _createFunc = () => new EmailClient(endpoint, credential, options);

    public EmailClient Create() => _createFunc.Invoke();
}
