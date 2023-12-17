namespace Zaabee.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider To(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.To.Add(new EmailAddress(address, name));
        return emailProvider;
    }

    public static IEmailProvider To(
        this IEmailProvider emailProvider,
        IEnumerable<EmailAddress> emailAddresses
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.To.AddRange(emailAddresses);
        return emailProvider;
    }

    public static IEmailProvider Cc(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.Cc.Add(new EmailAddress(address, name));
        return emailProvider;
    }

    public static IEmailProvider Cc(
        this IEmailProvider emailProvider,
        IEnumerable<EmailAddress> emailAddresses
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.Cc.AddRange(emailAddresses);
        return emailProvider;
    }

    public static IEmailProvider Bcc(
        this IEmailProvider emailProvider,
        string address,
        string? name = null
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.Bcc.Add(new EmailAddress(address, name));
        return emailProvider;
    }

    public static IEmailProvider Bcc(
        this IEmailProvider emailProvider,
        IEnumerable<EmailAddress> emailAddresses
    )
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Recipients.Bcc.AddRange(emailAddresses);
        return emailProvider;
    }
}
