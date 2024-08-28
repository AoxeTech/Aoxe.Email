namespace Aoxe.Aws.SimpleEmail;

public class AwsSimpleEmailProvider(IAmazonSimpleEmailServiceV2 sesClient) : IEmailProvider
{
    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    ) => await sesClient.SendEmailAsync(Factory.Create(email), cancellationToken);

    public void Dispose()
    {
        sesClient.Dispose();
    }
}
