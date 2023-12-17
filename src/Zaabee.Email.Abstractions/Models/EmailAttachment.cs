namespace Zaabee.Email.Abstractions.Models;

public class EmailAttachment
{
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public byte[] Content { get; set; } = Array.Empty<byte>();
}
