namespace IEmailServiceDal;

public interface IEmailDeliveryRecordDal : IDal
{
    Task AddAsync(EmailDeliveryRecord emailDeliveryRecord);
    Task DeleteAsync(string id);
    Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord);
    Task<EmailDeliveryRecord?> GetAsync(string id);
}
