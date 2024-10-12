namespace Aoxe.SmtpClient;

public class SmtpClientFactory : ISmtpClientFactory
{
    private readonly Func<System.Net.Mail.SmtpClient> _createFunc;

    public SmtpClientFactory(
        string host,
        int port = 25,
        string? userName = null,
        string? password = null,
        bool? enableSsl = null
    ) =>
        _createFunc = () =>
        {
            var smtpClient = new System.Net.Mail.SmtpClient(host, port);
            if (userName is not null && password is not null)
                smtpClient.Credentials = new NetworkCredential(userName, password);
            if (enableSsl.HasValue)
                smtpClient.EnableSsl = enableSsl.Value;
            return smtpClient;
        };

    public System.Net.Mail.SmtpClient Create() => _createFunc.Invoke();
}
