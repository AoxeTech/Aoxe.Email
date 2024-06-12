namespace Aoxe.Email.Abstractions;

public interface IEmailProvider : IDisposable
{
    Models.Email? Email { get; set; }
    ValueTask SendAsync(Models.Email email, CancellationToken cancellationToken = default);
    void CleanEmail();
}
