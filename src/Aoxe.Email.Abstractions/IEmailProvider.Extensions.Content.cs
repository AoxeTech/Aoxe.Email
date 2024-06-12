namespace Aoxe.Email.Abstractions;

public static partial class EmailProviderExtensions
{
    public static IEmailProvider Subject(this IEmailProvider emailProvider, string subject)
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Content.Subject = subject;
        return emailProvider;
    }

    public static IEmailProvider TextBody(this IEmailProvider emailProvider, string textBody)
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Content.TextBody = textBody;
        return emailProvider;
    }

    public static IEmailProvider HtmlBody(this IEmailProvider emailProvider, string htmlBody)
    {
        emailProvider.Email ??= new Models.Email();
        emailProvider.Email.Content.HtmlBody = htmlBody;
        return emailProvider;
    }
}
