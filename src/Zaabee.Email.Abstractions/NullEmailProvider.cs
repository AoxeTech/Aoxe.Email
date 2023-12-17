namespace Zaabee.Email.Abstractions;

public class NullEmailProvider : IEmailProvider
{
    public Models.Email? Email { get; set; }

    public ValueTask SendAsync(
        Models.Email emailCommand,
        CancellationToken cancellationToken = default
    ) => new();

    public void CleanEmail()
    {
        Email = null;
    }

    public void Dispose() { }
}
