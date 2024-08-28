namespace Aoxe.Email.Abstractions.Models;

public class EmailAttachment
{
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = Defaults.ContentType;
    public byte[] Content { get; set; } = [];
}
