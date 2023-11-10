namespace EmailServiceModel;

public class EmailDeliveryRecord : ModelBase
{
    public string FromEmail { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public List<string> ToEmails { get; set; } = new();

    public List<string> CcEmails { get; set; } = new();

    public List<string> BccEmails { get; set; } = new();
    
    public EmailDeliveryRecord(){}

    public EmailDeliveryRecord(Email email)
    {
        Id = email.Id;
        FromEmail = email.From.Address;
        DisplayName = email.From.Name;
        ToEmails = email.Recipients.To.Select(to => to.Address).ToList();
        CcEmails = email.Recipients.Cc.Select(cc => cc.Address).ToList();
        BccEmails = email.Recipients.Bcc.Select(bcc => bcc.Address).ToList();
    }
}