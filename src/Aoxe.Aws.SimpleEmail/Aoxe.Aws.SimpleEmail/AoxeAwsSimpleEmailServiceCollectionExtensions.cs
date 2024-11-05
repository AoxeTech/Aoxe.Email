namespace Aoxe.Aws.SimpleEmail;

public static class AoxeAwsSimpleEmailServiceCollectionExtensions
{
    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        RegionEndpoint? region = null
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(region)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AmazonSimpleEmailServiceV2Config config
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(config)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AWSCredentials credentials,
        RegionEndpoint? region = null
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(credentials, region)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AWSCredentials credentials,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(credentials, clientConfig)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        RegionEndpoint? region = null
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, region)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, clientConfig)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        RegionEndpoint? region = null
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region)
        ));

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        serviceCollection.AddScopedWithLazy<IEmailProvider>(_ => new AwsSimpleEmailProvider(
            new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, clientConfig)
        ));
}
