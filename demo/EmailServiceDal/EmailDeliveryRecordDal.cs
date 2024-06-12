namespace EmailServiceDal;

public class EmailDeliveryRecordDal : IEmailDeliveryRecordDal
{
    private readonly IAoxeMongoClient _AoxeMongoClient;

    public EmailDeliveryRecordDal(IAoxeMongoClient AoxeMongoClient)
    {
        _AoxeMongoClient = AoxeMongoClient;
    }

    public async Task AddAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _AoxeMongoClient.AddAsync(emailDeliveryRecord);

    public async Task DeleteAsync(string id)
    {
        var emailDeliveryRecord = await GetAsync(id);
        if (emailDeliveryRecord is not null)
            await _AoxeMongoClient.DeleteAsync(emailDeliveryRecord);
    }

    public async Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _AoxeMongoClient.UpdateAsync(emailDeliveryRecord);

    public Task<EmailDeliveryRecord?> GetAsync(string id) =>
        Task.FromResult(
            _AoxeMongoClient.GetQueryable<EmailDeliveryRecord>().FirstOrDefault(p => p.Id == id)
        );
}
