namespace Aoxe.Aws.SimpleEmail.Provider;

public sealed class AwsSimpleEmailProvider(ISesClientFactory esClientFactory) : IEmailProvider
{
    private IAmazonSimpleEmailServiceV2? _sesClient;

    public async ValueTask SendAsync(
        Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    )
    {
        _sesClient ??= esClientFactory.Create();
        await _sesClient.SendEmailAsync(email.ToSendEmailRequest(), cancellationToken);
    }

    public void Dispose() => _sesClient?.Dispose();
}
