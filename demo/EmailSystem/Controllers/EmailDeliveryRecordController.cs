namespace EmailSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailDeliveryRecordController : ControllerBase
{
    private readonly EmailDeliverRecordBll _emailDeliverRecordBll;

    public EmailDeliveryRecordController(EmailDeliverRecordBll emailDeliverRecordBll)
    {
        _emailDeliverRecordBll = emailDeliverRecordBll;
    }

    public async Task AddAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliverRecordBll.AddAsync(emailDeliveryRecord);

    public async Task DeleteAsync(string id) =>
        await _emailDeliverRecordBll.DeleteAsync(id);

    public async Task UpdateAsync(EmailDeliveryRecord emailDeliveryRecord) =>
        await _emailDeliverRecordBll.UpdateAsync(emailDeliveryRecord);

    public async Task<EmailDeliveryRecord?> GetAsync(string id) =>
        await _emailDeliverRecordBll.GetAsync(id);

    public async Task SendEmailAsync(SendEmailCommand? sendEmailCommand) =>
        await _emailDeliverRecordBll.SendEmailAsync(sendEmailCommand);
}