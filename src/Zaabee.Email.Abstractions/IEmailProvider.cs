namespace Zaabee.Email.Abstractions;

public interface IEmailProvider
{
    ValueTask SendAsync(SendEmailCommand emailCommand, CancellationToken cancellationToken = default);
}