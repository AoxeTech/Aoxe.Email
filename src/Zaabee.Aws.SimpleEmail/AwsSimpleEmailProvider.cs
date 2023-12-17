namespace Zaabee.Aws.SimpleEmail;

public class AwsSimpleEmailProvider(IAmazonSimpleEmailServiceV2 sesClient) : IEmailProvider
{
    public Email.Abstractions.Models.Email? Email { get; set; }

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    ) => await sesClient.SendEmailAsync(Factory.Create(email), cancellationToken);

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose()
    {
        sesClient.Dispose();
    }
}
