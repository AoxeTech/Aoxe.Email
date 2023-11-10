namespace Zaabee.Email.Abstractions.Models;

public class EmailContent
{
    public string Subject { get; set; } = string.Empty;
    public string PlainText { get; set; } = string.Empty;
    public string Html { get; set; } = string.Empty;
}