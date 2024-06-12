namespace EmailSystem;

public class EmailSystemBackgroundService : BackgroundService
{
    private readonly IAoxeRabbitMqClient _messageBus;
    private readonly IServiceProvider _serviceProvider;

    public EmailSystemBackgroundService(
        IAoxeRabbitMqClient messageBus,
        IServiceProvider serviceProvider
    )
    {
        _messageBus = messageBus;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageBus.ReceiveCommand<Email>(() =>
        {
            using var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<EmailDeliverRecordBll>().SendEmailAsync;
        });
        return Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _messageBus.Dispose();
        await base.StopAsync(cancellationToken);
    }
}
