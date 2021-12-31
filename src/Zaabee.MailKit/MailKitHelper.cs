using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Zaabee.MailKit
{
    public class MailKitHelper
    {
        private string? _host;
        public string GetHost => _host;
        private int _port;
        public int GetPort => _port;
        private bool _ssl;
        public bool GetSsl => _ssl;
        private string? _userName;
        public string GetUserName => _userName;
        private string? _password;
        public string GetPassword => _password;

        public MailKitHelper Host(string host)
        {
            _host = host;
            return this;
        }

        public MailKitHelper Port(int port)
        {
            _port = port;
            return this;
        }

        public MailKitHelper Ssl(bool ssl)
        {
            _ssl = ssl;
            return this;
        }

        public MailKitHelper UserName(string userName)
        {
            _userName = userName;
            return this;
        }

        public MailKitHelper Password(string password)
        {
            _password = password;
            return this;
        }

        public void Send(Mail? sendMessage, SmtpClient? smtpClient = null)
        {
            if (sendMessage is null) return;
            var client = smtpClient ?? new SmtpClient();
            if (_ssl) client.Connect(_host, _port, SecureSocketOptions.StartTls);
            else client.Connect(_host, _port, _ssl);
            client.Authenticate(_userName, _password);
            client.Send(sendMessage.CreateMail());

            if (smtpClient is not null) return;
            client.Disconnect(true);
            client.Dispose();
        }

        public async Task SendAsync(Mail? sendMessage, SmtpClient? smtpClient = null)
        {
            if (sendMessage is null) return;
            var client = smtpClient ?? new SmtpClient();
            if (_ssl) await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
            else await client.ConnectAsync(_host, _port, _ssl);
            await client.AuthenticateAsync(_userName, _password);
            await client.SendAsync(sendMessage.CreateMail());

            if (smtpClient is null)
            {
                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}