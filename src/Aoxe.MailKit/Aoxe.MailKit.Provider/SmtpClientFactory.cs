namespace Aoxe.MailKit.Provider;

public class SmtpClientFactory : ISmtpClientFactory
{
    private readonly Func<ValueTask<IMailTransport>> _createFunc;

#if NET8_0_OR_GREATER
    public SmtpClientFactory(
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        IProtocolLogger? protocolLogger = null,
        IMeterFactory? meterFactory = null
    )
    {
        if (protocolLogger is null)
            _createFunc = async () =>
            {
                var smtpClient = new SmtpClient();
                await InitSmtpClientAsync(smtpClient, host, port, userName, password);
                return smtpClient;
            };
        else if (meterFactory is null)
            _createFunc = async () =>
            {
                var smtpClient = new SmtpClient(protocolLogger);
                await InitSmtpClientAsync(smtpClient, host, port, userName, password);
                return smtpClient;
            };
        else
            _createFunc = async () =>
            {
                var smtpClient = new SmtpClient(protocolLogger, meterFactory);
                await InitSmtpClientAsync(smtpClient, host, port, userName, password);
                return smtpClient;
            };
    }
#else
    public SmtpClientFactory(
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        IProtocolLogger? protocolLogger = null
    )
    {
        if (protocolLogger is null)
            _createFunc = async () =>
            {
                var smtpClient = new SmtpClient();
                await InitSmtpClientAsync(smtpClient, host, port, userName, password);
                return smtpClient;
            };
        else
            _createFunc = async () =>
            {
                var smtpClient = new SmtpClient(protocolLogger);
                await InitSmtpClientAsync(smtpClient, host, port, userName, password);
                return smtpClient;
            };
    }
#endif

    public ValueTask<IMailTransport> CreateAsync() => _createFunc.Invoke();

    private async ValueTask InitSmtpClientAsync(
        SmtpClient smtpClient,
        string host,
        int port = 25,
        string? userName = null,
        string? password = null
    )
    {
        if (!smtpClient.IsConnected)
            await smtpClient.ConnectAsync(host, port);
        if (!smtpClient.IsAuthenticated && userName is not null && password is not null)
            await smtpClient.AuthenticateAsync(userName, password);
    }
}
