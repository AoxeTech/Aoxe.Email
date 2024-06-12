namespace Aoxe.Email.Abstractions.Models;

public class EmailAttachment
{
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = $"{MediaType.Application}/{MediaSubType.OctetStream}";
    public byte[] Content { get; set; } = [];
}
