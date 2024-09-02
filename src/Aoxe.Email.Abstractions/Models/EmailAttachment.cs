namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailAttachment(
    string name,
    byte[] content,
    string mediaType = Defaults.MediaTypeApplication,
    string subMediaType = Defaults.MediaSubTypeOctetStream
)
{
    public string Name { get; } = name;
    public byte[] Content { get; } = content;
    public string MediaType { get; } = mediaType;
    public string SubMediaType { get; } = subMediaType;
    public string ContentType => $"{MediaType}/{SubMediaType}";
}
