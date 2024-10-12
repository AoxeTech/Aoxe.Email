namespace Aoxe.Aws.SimpleEmail.Provider;

public class SesClientFactory : ISesClientFactory
{
    private readonly Func<IAmazonSimpleEmailServiceV2> _createFunc;

    public SesClientFactory(RegionEndpoint? region = null) =>
        _createFunc = region is null
            ? () => new AmazonSimpleEmailServiceV2Client()
            : () => new AmazonSimpleEmailServiceV2Client(region);

    public SesClientFactory(AmazonSimpleEmailServiceV2Config config) =>
        _createFunc = () => new AmazonSimpleEmailServiceV2Client(config);

    public SesClientFactory(AWSCredentials credentials, RegionEndpoint? region = null) =>
        _createFunc = region is null
            ? () => new AmazonSimpleEmailServiceV2Client(credentials)
            : () => new AmazonSimpleEmailServiceV2Client(credentials, region);

    public SesClientFactory(
        AWSCredentials credentials,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) => _createFunc = () => new AmazonSimpleEmailServiceV2Client(credentials, clientConfig);

    public SesClientFactory(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        RegionEndpoint? region = null
    ) =>
        _createFunc = region is null
            ? () => new AmazonSimpleEmailServiceV2Client(awsAccessKeyId, awsSecretAccessKey)
            : () =>
                new AmazonSimpleEmailServiceV2Client(awsAccessKeyId, awsSecretAccessKey, region);

    public SesClientFactory(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        _createFunc = () =>
            new AmazonSimpleEmailServiceV2Client(awsAccessKeyId, awsSecretAccessKey, clientConfig);

    public SesClientFactory(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        RegionEndpoint? region = null
    ) =>
        _createFunc = region is null
            ? () =>
                new AmazonSimpleEmailServiceV2Client(
                    awsAccessKeyId,
                    awsSecretAccessKey,
                    awsSessionToken
                )
            : () =>
                new AmazonSimpleEmailServiceV2Client(
                    awsAccessKeyId,
                    awsSecretAccessKey,
                    awsSessionToken,
                    region
                );

    public SesClientFactory(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        _createFunc = () =>
            new AmazonSimpleEmailServiceV2Client(
                awsAccessKeyId,
                awsSecretAccessKey,
                awsSessionToken,
                clientConfig
            );

    public IAmazonSimpleEmailServiceV2 Create() => _createFunc.Invoke();
}
