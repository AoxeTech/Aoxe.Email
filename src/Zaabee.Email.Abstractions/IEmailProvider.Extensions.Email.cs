namespace Zaabee.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider Email(this IEmailProvider emailProvider, Models.Email email)
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email = email;
        return emailProvider;
    }
}
