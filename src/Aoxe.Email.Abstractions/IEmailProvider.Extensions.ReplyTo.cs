namespace Aoxe.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider ReplyTo(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.ReplyTo.Add(new EmailAddress(address, name));
        return emailProvider;
    }

    public static IEmailProvider ReplyTo(
        this IEmailProvider emailProvider,
        IEnumerable<EmailAddress> emailAddresses
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.ReplyTo.AddRange(emailAddresses);
        return emailProvider;
    }
}
