namespace Aoxe.Email.Abstractions;

public interface IEmailProvider : IDisposable
{
    ValueTask SendAsync(Models.Email email, CancellationToken cancellationToken = default);
}
