namespace EmailServiceBll;

public class EmailDeliverRecordBll : IBll
{
    private readonly IEmailDeliveryRecordDal _emailDeliveryRecordDal;

    public EmailDeliverRecordBll(IEmailDeliveryRecordDal emailDeliveryRecordDal)
    {
        _emailDeliveryRecordDal = emailDeliveryRecordDal;
    }

    public async Task AddAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliveryRecordDal.AddAsync(emailDeliveryRecord);

    public async Task DeleteAsync(string id) =>
        await _emailDeliveryRecordDal.DeleteAsync(id);

    public async Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliveryRecordDal.UpdateAsync(emailDeliveryRecord);

    public async Task<EmailDeliveryRecord?> GetAsync(string id) =>
        await _emailDeliveryRecordDal.GetAsync(id);

    public async Task SendEmailAsync(SendEmailCommand sendEmailCommand)
    {
        var mailKitHelper = new MailKitHelper();
        var mail = new Mail();
        mail.From(sendEmailCommand.FromEmail)
            .Subject($"email test({DateTime.Now}+{Guid.NewGuid()})")
            .Body(sendEmailCommand.Body)
            .To(sendEmailCommand.ToEmails)
            .Cc(sendEmailCommand.CcEmails)
            .Bcc(sendEmailCommand.BccEmails);
        await mailKitHelper.Host("Your SMTP server's IP.")
            .Port(587)
            .UserName("The user name for NetworkCredential")
            .Password("The password for NetworkCredential")
            .Ssl(true)
            .SendAsync(mail);
    }
}