using Zaabee.Email.Abstractions.Contracts;

namespace EmailServiceModel;

public class EmailDeliveryRecord : ModelBase
{
    public string FromEmail { get; set; } = string.Empty;

    public string DisplayName { get; set; } = string.Empty;

    public List<string> ToEmails { get; set; } = new();

    public List<string> CcEmails { get; set; } = new();

    public List<string> BccEmails { get; set; } = new();
    
    public EmailDeliveryRecord(){}

    public EmailDeliveryRecord(SendEmailCommand emailCommand)
    {
        Id = emailCommand.Id;
        FromEmail = emailCommand.From.Address;
        DisplayName = emailCommand.From.Name;
        ToEmails = emailCommand.Recipients.To.Select(to => to.Address).ToList();
        CcEmails = emailCommand.Recipients.Cc.Select(cc => cc.Address).ToList();
        BccEmails = emailCommand.Recipients.Bcc.Select(bcc => bcc.Address).ToList();
    }
}