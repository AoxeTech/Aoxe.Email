namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailAddress
{
    public string Name { get; } = string.Empty;
    public string Address { get; } = string.Empty;

    public EmailAddress() { }

    public EmailAddress(string address, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(
                $"{nameof(address)} can not be null or empty or white space."
            );
        Address = address.Trim();
        Name = name?.Trim() ?? string.Empty;
    }
}
