namespace Aoxe.Email.Abstractions;

public class NullEmailProvider : IEmailProvider
{
    public ValueTask SendAsync(Models.Email email, CancellationToken cancellationToken = default) =>
        new();

    public void Dispose() { }
}
