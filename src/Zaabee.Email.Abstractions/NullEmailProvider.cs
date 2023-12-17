namespace Zaabee.Email.Abstractions;

public class NullEmailProvider : IEmailProvider
{
    public Models.Email? Email { get; set; }

    public ValueTask SendAsync(
        Models.Email email,
        CancellationToken cancellationToken = default
    ) => new();

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose() { }
}
