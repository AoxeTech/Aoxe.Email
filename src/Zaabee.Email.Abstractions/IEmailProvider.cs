namespace Zaabee.Email.Abstractions;

public interface IEmailProvider
{
    ValueTask SendAsync(EmailCommand emailCommand, CancellationToken cancellationToken = default);
}