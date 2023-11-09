namespace EmailServiceBll;

public class EmailDeliverRecordBll : IBll
{
    private readonly IEmailDeliveryRecordDal _emailDeliveryRecordDal;
    private readonly IEmailProvider _emailProvider;

    public EmailDeliverRecordBll(IEmailDeliveryRecordDal emailDeliveryRecordDal,
        IEmailProvider emailProvider)
    {
        _emailDeliveryRecordDal = emailDeliveryRecordDal;
        _emailProvider = emailProvider;
    }

    public async Task AddAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliveryRecordDal.AddAsync(emailDeliveryRecord);

    public async Task DeleteAsync(string id) =>
        await _emailDeliveryRecordDal.DeleteAsync(id);

    public async Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliveryRecordDal.UpdateAsync(emailDeliveryRecord);

    public async Task<EmailDeliveryRecord?> GetAsync(string id) =>
        await _emailDeliveryRecordDal.GetAsync(id);

    public async Task SendEmailAsync(SendEmailCommand? sendEmailCommand)
    {
        if (sendEmailCommand is null) return;
        await _emailProvider.SendAsync(sendEmailCommand);
        await _emailDeliveryRecordDal.AddAsync(new EmailDeliveryRecord(sendEmailCommand));
    }
}