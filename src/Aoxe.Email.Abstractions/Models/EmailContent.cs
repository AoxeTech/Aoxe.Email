namespace Aoxe.Email.Abstractions.Models;

public sealed class EmailContent
{
    public string Subject { get; set; } = string.Empty;
    public string TextBody { get; set; } = string.Empty;
    public string HtmlBody { get; set; } = string.Empty;
}
