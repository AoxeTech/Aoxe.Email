namespace Zaabee.Aws.SimpleEmail;

public class AwsSimpleEmailProvider : IEmailProvider
{
    private readonly IAmazonSimpleEmailServiceV2 _sesClient;

    public AwsSimpleEmailProvider(IAmazonSimpleEmailServiceV2 sesClient)
    {
        _sesClient = sesClient;
    }

    public async ValueTask SendAsync(SendEmailCommand emailCommand, CancellationToken cancellationToken = default) =>
        await _sesClient.SendEmailAsync(Factory.Create(emailCommand), cancellationToken);
}