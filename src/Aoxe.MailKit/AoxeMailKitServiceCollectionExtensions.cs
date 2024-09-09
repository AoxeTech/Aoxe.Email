namespace Aoxe.MailKit;

public static class AoxeMailKitServiceCollectionExtensions
{
#if NET8_0_OR_GREATER
    public static IServiceCollection AddMailKit(
        this IServiceCollection serviceCollection,
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        IProtocolLogger? protocolLogger = null,
        IMeterFactory? meterFactory = null
    ) =>
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new MailKitProvider(
                    new SmtpClientFactory(
                        host,
                        port,
                        userName,
                        password,
                        protocolLogger,
                        meterFactory
                    )
                )
        );
#else
    public static IServiceCollection AddMailKit(
        this IServiceCollection serviceCollection,
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        IProtocolLogger? protocolLogger = null
    ) =>
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new MailKitProvider(
                    new SmtpClientFactory(host, port, userName, password, protocolLogger)
                )
        );
#endif
}
