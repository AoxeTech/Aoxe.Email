namespace Aoxe.Email.Abstractions;

public static class EmailExtensions
{
    public static ValueTask SendByAsync(
        this Models.Email email,
        IEmailProvider emailProvider,
        CancellationToken cancelToken = default
    ) => emailProvider.SendAsync(email, cancelToken);
}
