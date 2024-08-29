namespace Aoxe.MailKit;

public static class AoxeMailKitServiceCollectionExtensions
{
    public static IServiceCollection AddMailKit(
        this IServiceCollection serviceCollection,
        string host,
        int port = 2525,
        string? userName = null,
        string? password = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(_ => new MailKitProvider(
            host,
            port,
            userName,
            password
        ));
        return serviceCollection;
    }
}
