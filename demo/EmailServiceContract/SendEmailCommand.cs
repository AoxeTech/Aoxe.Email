namespace EmailServiceContract;

public class SendEmailCommand
{
    public string FromEmail { get; set; }

    public string DisplayName { get; set; }

    public List<string> ToEmails { get; set; }

    public List<string> CcEmails { get; set; }

    public List<string> BccEmails { get; set; }
}