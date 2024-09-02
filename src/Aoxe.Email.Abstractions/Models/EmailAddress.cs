namespace Aoxe.Email.Abstractions.Models;

public sealed record EmailAddress
{
    public string Name { get; } = string.Empty;
    public string Address { get; } = string.Empty;

    public EmailAddress() { }

    public EmailAddress(string address, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException($"{nameof(address)} can not be null or white space.");
        Address = address.Trim();
        Name = name?.Trim() ?? string.Empty;
    }
}
