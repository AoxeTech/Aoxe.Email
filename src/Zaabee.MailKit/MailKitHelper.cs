using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Zaabee.MailKit
{
    public class MailKitHelper
    {
        private string _host;
        private int _port;
        private bool _ssl;
        private string _userName;
        private string _password;

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

        public void Send(SendMessage sendMessage)
        {
            if (sendMessage is null) return;
            using (var client = new SmtpClient())
            {
                if (_ssl) client.Connect(_host, _port, SecureSocketOptions.StartTls);
                else client.Connect(_host, _port, _ssl);
                client.Authenticate(_userName, _password);
                client.Send(sendMessage.CreateMail());
                client.Disconnect(true);
            }
        }

        public async Task SendAsync(SendMessage sendMessage)
        {
            if (sendMessage is null) return;
            using (var client = new SmtpClient())
            {
                if (_ssl) await client.ConnectAsync(_host, _port, SecureSocketOptions.StartTls);
                else await client.ConnectAsync(_host, _port, _ssl);
                await client.AuthenticateAsync(_userName, _password);
                await client.SendAsync(sendMessage.CreateMail());
                await client.DisconnectAsync(true);
            }
        }
    }
}