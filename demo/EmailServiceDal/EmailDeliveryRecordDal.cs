namespace EmailServiceDal;

public class EmailDeliveryRecordDal : IEmailDeliveryRecordDal
{
    private readonly IZaabeeMongoClient _zaabeeMongoClient;

    public EmailDeliveryRecordDal(IZaabeeMongoClient zaabeeMongoClient)
    {
        _zaabeeMongoClient = zaabeeMongoClient;
    }

    public async Task AddAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _zaabeeMongoClient.AddAsync(emailDeliveryRecord);

    public async Task DeleteAsync(string id)
    {
        var emailDeliveryRecord = await GetAsync(id);
        if (emailDeliveryRecord is not null)
            await _zaabeeMongoClient.DeleteAsync(emailDeliveryRecord);
    }

    public async Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _zaabeeMongoClient.UpdateAsync(emailDeliveryRecord);

    public Task<EmailDeliveryRecord?> GetAsync(string id) =>
        Task.FromResult(
            _zaabeeMongoClient.GetQueryable<EmailDeliveryRecord>().FirstOrDefault(p => p.Id == id)
        );
}
