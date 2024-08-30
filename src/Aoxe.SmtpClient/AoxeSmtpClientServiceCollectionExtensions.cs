namespace Aoxe.SmtpClient;

public static class AoxeSmtpClientServiceCollectionExtensions
{
    public static IServiceCollection AddSmtpClient(
        this IServiceCollection serviceCollection,
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        bool? enableSsl = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(_ => new SmtpProvider(
            new SmtpClientFactory(host, port, userName, password, enableSsl)
        ));
        return serviceCollection;
    }
}
