namespace Zaabee.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider Sender(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Sender = new EmailAddress(address, name);
        return emailProvider;
    }
}
