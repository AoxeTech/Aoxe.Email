namespace Aoxe.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static async ValueTask SendAsync(
        this IEmailProvider emailProvider,
        CancellationToken cancellationToken = default
    )
    {
        if (emailProvider.Email is null)
            throw new NullReferenceException($"{nameof(emailProvider.Email)} is null");
        await emailProvider.SendAsync(emailProvider.Email, cancellationToken);
        emailProvider.Email = null;
    }
}
