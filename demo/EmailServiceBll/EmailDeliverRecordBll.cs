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

    public async Task<EmailDeliveryRecord> GetAsync(string id) =>
        await _emailDeliveryRecordDal.GetAsync(id);

    public async Task SendEmailAsync(SendEmailCommand? sendEmailCommand)
    {
        var mailKitHelper = new MailKitHelper();
        var mail = new Mail();
        mail.From("From email")
            .Subject($"email test({DateTime.Now}+{Guid.NewGuid()})")
            .Body(@"Across the Great Wall we can reach every corner in the world.")
            .To(new List<string> { "123@live.com", "456@gmail.com" })
            .Cc("789@hotmail.com")
            .Bcc("123@163.com");
        await mailKitHelper.Host("Your SMTP server's IP.")
            .Port(587)
            .UserName("The user name for NetworkCredential")
            .Password("The password for NetworkCredential")
            .Ssl(true)
            .SendAsync(mail);
    }
}