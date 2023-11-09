namespace Zaabee.Email.Abstractions;

public interface IEmailProvider : IDisposable
{
    ValueTask SendAsync(SendEmailCommand emailCommand, CancellationToken cancellationToken = default);
}