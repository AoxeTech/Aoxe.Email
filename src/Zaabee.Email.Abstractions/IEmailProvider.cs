using Zaabee.Email.Abstractions.Models;

namespace Zaabee.Email.Abstractions;

public interface IEmailProvider : IDisposable
{
    ValueTask SendAsync(Models.Email emailCommand, CancellationToken cancellationToken = default);
}