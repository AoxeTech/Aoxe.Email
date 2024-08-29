namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailContent
{
    public string Subject { get; internal set; } = string.Empty;
    public string TextBody { get; internal set; } = string.Empty;
    public string HtmlBody { get; internal set; } = string.Empty;
}
