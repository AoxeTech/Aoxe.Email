namespace Aoxe.Azure.Email;

public static class AoxeAzureEmailServiceCollectionExtensions
{
    public static IServiceCollection AddAzureEmail(
        this IServiceCollection serviceCollection,
        string connectionString,
        EmailClientOptions? options = null
    ) =>
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AzureEmailProvider(new EmailClientFactory(connectionString, options))
        );

    public static IServiceCollection AddAzureEmail(
        this IServiceCollection serviceCollection,
        Uri endpoint,
        AzureKeyCredential credential,
        EmailClientOptions? options = null
    ) =>
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AzureEmailProvider(new EmailClientFactory(endpoint, credential, options))
        );

    public static IServiceCollection AddAzureEmail(
        this IServiceCollection serviceCollection,
        Uri endpoint,
        TokenCredential credential,
        EmailClientOptions? options = null
    ) =>
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AzureEmailProvider(new EmailClientFactory(endpoint, credential, options))
        );
}
