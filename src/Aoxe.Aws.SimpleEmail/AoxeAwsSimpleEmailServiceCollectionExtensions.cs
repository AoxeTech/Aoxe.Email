namespace Aoxe.Aws.SimpleEmail;

public static class AoxeAwsSimpleEmailServiceCollectionExtensions
{
    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        RegionEndpoint? region = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AwsSimpleEmailProvider(new SesClientFactory(region))
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AmazonSimpleEmailServiceV2Config config
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AwsSimpleEmailProvider(new SesClientFactory(config))
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AWSCredentials credentials,
        RegionEndpoint? region = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AwsSimpleEmailProvider(new SesClientFactory(credentials, region))
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        AWSCredentials credentials,
        AmazonSimpleEmailServiceV2Config clientConfig
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ => new AwsSimpleEmailProvider(new SesClientFactory(credentials, clientConfig))
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        RegionEndpoint? region = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new AwsSimpleEmailProvider(
                    new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, region)
                )
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        AmazonSimpleEmailServiceV2Config clientConfig
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new AwsSimpleEmailProvider(
                    new SesClientFactory(awsAccessKeyId, awsSecretAccessKey, clientConfig)
                )
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        RegionEndpoint? region = null
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new AwsSimpleEmailProvider(
                    new SesClientFactory(
                        awsAccessKeyId,
                        awsSecretAccessKey,
                        awsSessionToken,
                        region
                    )
                )
        );
        return serviceCollection;
    }

    public static IServiceCollection AddAwsSimpleEmail(
        this IServiceCollection serviceCollection,
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        AmazonSimpleEmailServiceV2Config clientConfig
    )
    {
        serviceCollection.AddScoped<IEmailProvider>(
            _ =>
                new AwsSimpleEmailProvider(
                    new SesClientFactory(
                        awsAccessKeyId,
                        awsSecretAccessKey,
                        awsSessionToken,
                        clientConfig
                    )
                )
        );
        return serviceCollection;
    }
}
