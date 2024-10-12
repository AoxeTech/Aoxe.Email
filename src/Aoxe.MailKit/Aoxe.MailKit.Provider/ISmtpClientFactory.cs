namespace Aoxe.MailKit.Provider;

public interface ISmtpClientFactory
{
    ValueTask<IMailTransport> CreateAsync();
}
