namespace Aoxe.SmtpClient;

public interface ISmtpClientFactory
{
    System.Net.Mail.SmtpClient Create();
}
