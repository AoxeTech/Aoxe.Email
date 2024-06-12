namespace Aoxe.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider From(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.From = new EmailAddress(address, name);
        return emailProvider;
    }
}
