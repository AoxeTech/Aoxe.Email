using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Zaabee.EmailUtility
{
    public class EmailHelper
    {
        private bool _isBodyHtml = true;

        private Encoding _bodyEncoding = Encoding.UTF8;

        private MailPriority _mailPriority = MailPriority.Normal;

        private string _from;

        private string _host;

        private int _port;

        private string _userName;

        private string _password;

        private List<string> _toList;

        private List<string> _ccList;

        private List<string> _bccList;

        private List<Attachment> _attachments;

        private string _subject;

        private string _body;

        /// <summary>
        /// From address
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public EmailHelper From(string from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// SMTP server's IP
        /// </summary>
        /// <param name="smtpIp"></param>
        /// <returns></returns>
        public EmailHelper Host(string smtpIp)
        {
            _host = smtpIp;
            return this;
        }

        /// <summary>
        /// SMTP server's port
        /// </summary>
        /// <param name="smtpPort"></param>
        /// <returns></returns>
        public EmailHelper Port(int smtpPort)
        {
            _port = smtpPort;
            return this;
        }

        /// <summary>
        /// The userName for NetworkCredential
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public EmailHelper UserName(string userName)
        {
            _userName = userName;
            return this;
        }

        /// <summary>
        /// The password for NetworkCredential
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public EmailHelper Password(string password)
        {
            _password = password;
            return this;
        }

        #region Recipients

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="toList"></param>
        /// <returns></returns>
        public EmailHelper To(IEnumerable<string> toList)
        {
            _toList = _toList ?? new List<string>();
            _toList.AddRange(toList);
            return this;
        }

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="tos"></param>
        /// <returns></returns>
        public EmailHelper To(params string[] tos)
        {
            _toList = _toList ?? new List<string>();
            _toList.AddRange(tos);
            return this;
        }

        #endregion

        #region Carbon copy

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="ccList"></param>
        /// <returns></returns>
        public EmailHelper Cc(IEnumerable<string> ccList)
        {
            _ccList = _ccList ?? new List<string>();
            _ccList.AddRange(ccList);
            return this;
        }

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="ccs"></param>
        /// <returns></returns>
        public EmailHelper Cc(params string[] ccs)
        {
            _ccList = _ccList ?? new List<string>();
            _ccList.AddRange(ccs);
            return this;
        }

        #endregion

        #region Blind carbon copy

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="bccList"></param>
        /// <returns></returns>
        public EmailHelper Bcc(IEnumerable<string> bccList)
        {
            _bccList = _bccList ?? new List<string>();
            _bccList.AddRange(bccList);
            return this;
        }

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="bccs"></param>
        /// <returns></returns>
        public EmailHelper Bcc(params string[] bccs)
        {
            _bccList = _bccList ?? new List<string>();
            _bccList.AddRange(bccs);
            return this;
        }

        #endregion

        #region Attachment

        public EmailHelper Attachment(FileStream stream, ContentType contentType = null)
        {
            _attachments = _attachments ?? new List<Attachment>();
            contentType = contentType ?? new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(stream, contentType));
            return this;
        }

        public EmailHelper Attachment(FileStream stream, string name)
        {
            _attachments = _attachments ?? new List<Attachment>();
            _attachments.Add(new Attachment(stream, name));
            return this;
        }

        public EmailHelper Attachment(FileStream stream, string name, string mediaType)
        {
            _attachments = _attachments ?? new List<Attachment>();
            _attachments.Add(new Attachment(stream, name, mediaType));
            return this;
        }

        public EmailHelper Attachment(string fileName)
        {
            _attachments = _attachments ?? new List<Attachment>();
            _attachments.Add(new Attachment(fileName));
            return this;
        }

        public EmailHelper Attachment(string fileName, ContentType contentType)
        {
            _attachments = _attachments ?? new List<Attachment>();
            contentType = contentType ?? new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(fileName, contentType));
            return this;
        }

        public EmailHelper Attachment(string fileName, string mediaType)
        {
            _attachments = _attachments ?? new List<Attachment>();
            _attachments.Add(new Attachment(fileName, mediaType));
            return this;
        }

        public EmailHelper Attachment(Stream stream, string name = null)
        {
            _attachments = _attachments ?? new List<Attachment>();
            _attachments.Add(new Attachment(stream, name));
            return this;
        }

        public EmailHelper Attachment(List<Tuple<Stream, string>> attachments)
        {
            _attachments = _attachments ?? new List<Attachment>();
            attachments?.ForEach(attachment => _attachments.Add(new Attachment(attachment.Item1, attachment.Item2)));
            return this;
        }

        #endregion

        public EmailHelper Subject(string subject)
        {
            _subject = subject;
            return this;
        }

        public EmailHelper Body(string body)
        {
            _body = body;
            return this;
        }

        public EmailHelper IsBodyHtml(bool isBodyHtml)
        {
            _isBodyHtml = isBodyHtml;
            return this;
        }

        public EmailHelper BodyEncoding(Encoding bodyEncoding)
        {
            _bodyEncoding = bodyEncoding;
            return this;
        }

        public EmailHelper Priority(MailPriority mailPriority)
        {
            _mailPriority = mailPriority;
            return this;
        }

        public void Send()
        {
            using (var client = new SmtpClient(_host, _port)
            {
                Credentials = new NetworkCredential(_userName, _password)
            })
            {
                client.Send(SetMail());
            }
        }

        private MailMessage SetMail()
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_from),
                Subject = _subject,
                IsBodyHtml = _isBodyHtml,
                BodyEncoding = _bodyEncoding,
                Body = _body,
                Priority = _mailPriority
            };

            _attachments?.ForEach(attachment => mail.Attachments.Add(attachment));
            _toList?.ForEach(to => mail.To.Add(new MailAddress(to)));
            _ccList?.ForEach(cc => mail.CC.Add(new MailAddress(cc)));
            _bccList?.ForEach(bcc => mail.Bcc.Add(new MailAddress(bcc)));

            return mail;
        }
    }
}