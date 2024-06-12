namespace Aoxe.Azure.Email;

public class AzureEmailProvider(EmailClient emailClient) : IEmailProvider
{
    public Aoxe.Email.Abstractions.Models.Email? Email { get; set; }

    public async ValueTask SendAsync(
        Aoxe.Email.Abstractions.Models.Email email,
        CancellationToken cancellationToken = default
    ) => await emailClient.SendAsync(WaitUntil.Completed, Factory.Create(email), cancellationToken);

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose() { }
}
