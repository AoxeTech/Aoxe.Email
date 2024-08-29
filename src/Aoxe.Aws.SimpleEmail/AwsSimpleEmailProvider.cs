namespace Aoxe.Aws.SimpleEmail;

public class AwsSimpleEmailProvider : IEmailProvider
{
    private readonly IAmazonSimpleEmailServiceV2 _sesClient;

    public AwsSimpleEmailProvider(RegionEndpoint? region = null) =>
        _sesClient = region is null
            ? new AmazonSimpleEmailServiceV2Client()
            : new AmazonSimpleEmailServiceV2Client(region);

    public AwsSimpleEmailProvider(AmazonSimpleEmailServiceV2Config config) =>
        _sesClient = new AmazonSimpleEmailServiceV2Client(config);

    public AwsSimpleEmailProvider(AWSCredentials credentials, RegionEndpoint? region = null) =>
        _sesClient = region is null
            ? new AmazonSimpleEmailServiceV2Client(credentials)
            : new AmazonSimpleEmailServiceV2Client(credentials, region);

    public AwsSimpleEmailProvider(
        AWSCredentials credentials,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) => _sesClient = new AmazonSimpleEmailServiceV2Client(credentials, clientConfig);

    public AwsSimpleEmailProvider(string awsAccessKeyId, string awsSecretAccessKey) =>
        _sesClient = new AmazonSimpleEmailServiceV2Client(awsAccessKeyId, awsSecretAccessKey);

    public AwsSimpleEmailProvider(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        RegionEndpoint region
    ) =>
        _sesClient = new AmazonSimpleEmailServiceV2Client(
            awsAccessKeyId,
            awsSecretAccessKey,
            region
        );

    public AwsSimpleEmailProvider(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        _sesClient = new AmazonSimpleEmailServiceV2Client(
            awsAccessKeyId,
            awsSecretAccessKey,
            clientConfig
        );

    public AwsSimpleEmailProvider(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        RegionEndpoint? region = null
    ) =>
        _sesClient = region is null
            ? new AmazonSimpleEmailServiceV2Client(
                awsAccessKeyId,
                awsSecretAccessKey,
                awsSessionToken
            )
            : new AmazonSimpleEmailServiceV2Client(
                awsAccessKeyId,
                awsSecretAccessKey,
                awsSessionToken,
                region
            );

    public AwsSimpleEmailProvider(
        string awsAccessKeyId,
        string awsSecretAccessKey,
        string awsSessionToken,
        AmazonSimpleEmailServiceV2Config clientConfig
    ) =>
        _sesClient = new AmazonSimpleEmailServiceV2Client(
            awsAccessKeyId,
            awsSecretAccessKey,
            awsSessionToken,
            clientConfig
        );

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    ) => await _sesClient.SendEmailAsync(Factory.Create(email), cancellationToken);

    public void Dispose() => _sesClient.Dispose();
}
