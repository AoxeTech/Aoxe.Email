namespace Aoxe.MailKit;

public interface ISmtpClientFactory
{
    ValueTask<IMailTransport> CreateAsync();
}
