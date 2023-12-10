namespace Zaabee.Aws.SimpleEmail;

public class AwsSimpleEmailProvider(IAmazonSimpleEmailServiceV2 sesClient) : IEmailProvider
{
    public async ValueTask SendAsync(Email.Abstractions.Models.Email emailCommand, CancellationToken cancellationToken = default) =>
        await sesClient.SendEmailAsync(Factory.Create(emailCommand), cancellationToken);

    public void Dispose()
    {
        sesClient.Dispose();
    }
}