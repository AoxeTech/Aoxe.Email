using Zaabee.Email.Abstractions.Models;

namespace Zaabee.Aws.SimpleEmail;

public class AwsSimpleEmailProvider : IEmailProvider
{
    private readonly IAmazonSimpleEmailServiceV2 _sesClient;

    public AwsSimpleEmailProvider(IAmazonSimpleEmailServiceV2 sesClient)
    {
        _sesClient = sesClient;
    }

    public async ValueTask SendAsync(Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await _sesClient.SendEmailAsync(Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {
        _sesClient.Dispose();
    }
}