namespace EmailServiceModel;

public class EmailDeliveryRecord : ModelBase
{
    public string FromEmail { get; set; }

    public string DisplayName { get; set; }

    public List<string> ToEmails { get; set; }

    public List<string> CcEmails { get; set; }

    public List<string> BccEmails { get; set; }

    public string TextBody { get; set; }

    public string HtmlBody { get; set; }

    public string DeliveryStatus { get; set; }

    public string DeliveryMessage { get; set; }

    public string AwsSesRegion { get; set; }

    public string SmtpHost { get; set; }

    public int SmtpPort { get; set; }
}