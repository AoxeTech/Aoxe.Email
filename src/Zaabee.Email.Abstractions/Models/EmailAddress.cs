namespace Zaabee.Email.Abstractions.Models;

public class EmailAddress
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    
    public EmailAddress()
    {
    }
    
    public EmailAddress(string address, string? name = null)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException($"{nameof(address)} can not be null or empty or white space.");
        Address = address.Trim();
        Name = name?.Trim() ?? string.Empty;
    }
}