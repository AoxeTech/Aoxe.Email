namespace Aoxe.SmtpClient.Provider;

public interface ISmtpClientFactory
{
    System.Net.Mail.SmtpClient Create();
}
