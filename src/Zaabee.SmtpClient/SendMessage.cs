using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Zaabee.SmtpClient
{
    public class SendMessage
    {
        private string _from;
        private string _subject;
        private bool _isBodyHtml = true;
        private Encoding _bodyEncoding = Encoding.UTF8;
        private string _body;
        private MailPriority _mailPriority = MailPriority.Normal;
        private List<string> _recipients;
        private List<string> _carbonCopies;
        private List<string> _blindCarbonCopies;
        private List<Attachment> _attachments;

        /// <summary>
        /// From address
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public SendMessage From(string from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public SendMessage To(IEnumerable<string> recipients)
        {
            _recipients ??= new List<string>();
            _recipients.AddRange(recipients);
            return this;
        }

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public SendMessage To(params string[] recipients)
        {
            _recipients ??= new List<string>();
            _recipients.AddRange(recipients);
            return this;
        }

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="carbonCopies"></param>
        /// <returns></returns>
        public SendMessage Cc(IEnumerable<string> carbonCopies)
        {
            _carbonCopies ??= new List<string>();
            _carbonCopies.AddRange(carbonCopies);
            return this;
        }

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="carbonCopies"></param>
        /// <returns></returns>
        public SendMessage Cc(params string[] carbonCopies)
        {
            _carbonCopies ??= new List<string>();
            _carbonCopies.AddRange(carbonCopies);
            return this;
        }

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="blindCarbonCopies"></param>
        /// <returns></returns>
        public SendMessage Bcc(IEnumerable<string> blindCarbonCopies)
        {
            _blindCarbonCopies ??= new List<string>();
            _blindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="blindCarbonCopies"></param>
        /// <returns></returns>
        public SendMessage Bcc(params string[] blindCarbonCopies)
        {
            _blindCarbonCopies ??= new List<string>();
            _blindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        public SendMessage Attachment(FileStream stream, ContentType contentType = null)
        {
            _attachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(stream, contentType));
            return this;
        }

        public SendMessage Attachment(Stream stream, string name)
        {
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(stream, name));
            return this;
        }

        public SendMessage Attachment(FileStream stream, string name, string mediaType)
        {
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(stream, name, mediaType));
            return this;
        }

        public SendMessage Attachment(string fileName)
        {
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(fileName));
            return this;
        }

        public SendMessage Attachment(string fileName, ContentType contentType)
        {
            _attachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(fileName, contentType));
            return this;
        }

        public SendMessage Attachment(string fileName, string mediaType)
        {
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(fileName, mediaType));
            return this;
        }

        public SendMessage Attachment(List<Tuple<Stream, string>> attachments)
        {
            _attachments ??= new List<Attachment>();
            attachments?.ForEach(attachment => _attachments.Add(new Attachment(attachment.Item1, attachment.Item2)));
            return this;
        }

        public SendMessage Subject(string subject)
        {
            _subject = subject;
            return this;
        }

        public SendMessage Body(string body)
        {
            _body = body;
            return this;
        }

        public SendMessage IsBodyHtml(bool isBodyHtml)
        {
            _isBodyHtml = isBodyHtml;
            return this;
        }

        public SendMessage BodyEncoding(Encoding bodyEncoding)
        {
            _bodyEncoding = bodyEncoding;
            return this;
        }

        public SendMessage Priority(MailPriority mailPriority)
        {
            _mailPriority = mailPriority;
            return this;
        }

        internal MailMessage CreateMail()
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
            _recipients?.ForEach(to => mail.To.Add(new MailAddress(to)));
            _carbonCopies?.ForEach(cc => mail.CC.Add(new MailAddress(cc)));
            _blindCarbonCopies?.ForEach(bcc => mail.Bcc.Add(new MailAddress(bcc)));

            return mail;
        }
    }
}