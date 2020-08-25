using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Zaabee.Extensions;

namespace Zaabee.SmtpClient
{
    public class Mail
    {
        private string _from;
        public string GetFrom => _from;
        private string _subject;
        public string GetSubject => _subject;
        private bool _isBodyHtml = true;
        public bool GetIsBodyHtml => _isBodyHtml;
        private Encoding _bodyEncoding = Encoding.UTF8;
        public Encoding GetBodyEncoding => _bodyEncoding;
        private string _body;
        public string GetBody => _body;
        private MailPriority _mailPriority = MailPriority.Normal;
        public MailPriority GetMailPriority => _mailPriority;
        private List<string> _recipients;
        public List<string> GetRecipients => _recipients;
        private List<string> _carbonCopies;
        public List<string> GetCarbonCopies => _carbonCopies;
        private List<string> _blindCarbonCopies;
        public List<string> GetBlindCarbonCopies => _blindCarbonCopies;
        private List<Attachment> _attachments;
        public List<Attachment> GetAttachments => _attachments;

        /// <summary>
        /// From address
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public Mail From(string from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public Mail To(IList<string> recipients)
        {
            if (recipients.IsNullOrEmpty()) return this;
            _recipients ??= new List<string>();
            _recipients.AddRange(recipients);
            return this;
        }

        /// <summary>
        /// Recipients
        /// </summary>
        /// <param name="recipients"></param>
        /// <returns></returns>
        public Mail To(params string[] recipients)
        {
            if (recipients.IsNullOrEmpty()) return this;
            _recipients ??= new List<string>();
            _recipients.AddRange(recipients);
            return this;
        }

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="carbonCopies"></param>
        /// <returns></returns>
        public Mail Cc(IList<string> carbonCopies)
        {
            if (carbonCopies.IsNullOrEmpty()) return this;
            _carbonCopies ??= new List<string>();
            _carbonCopies.AddRange(carbonCopies);
            return this;
        }

        /// <summary>
        /// Carbon copy
        /// </summary>
        /// <param name="carbonCopies"></param>
        /// <returns></returns>
        public Mail Cc(params string[] carbonCopies)
        {
            if (carbonCopies.IsNullOrEmpty()) return this;
            _carbonCopies ??= new List<string>();
            _carbonCopies.AddRange(carbonCopies);
            return this;
        }

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="blindCarbonCopies"></param>
        /// <returns></returns>
        public Mail Bcc(IList<string> blindCarbonCopies)
        {
            if (blindCarbonCopies.IsNullOrEmpty()) return this;
            _blindCarbonCopies ??= new List<string>();
            _blindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        /// <summary>
        /// Blind carbon copy
        /// </summary>
        /// <param name="blindCarbonCopies"></param>
        /// <returns></returns>
        public Mail Bcc(params string[] blindCarbonCopies)
        {
            if (blindCarbonCopies.IsNullOrEmpty()) return this;
            _blindCarbonCopies ??= new List<string>();
            _blindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        public Mail Attachment(FileStream stream, ContentType contentType = null)
        {
            if (stream is null) return this;
            _attachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(stream, contentType));
            return this;
        }

        public Mail Attachment(Stream stream, string name)
        {
            if (stream is null) return this;
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(stream, name));
            return this;
        }

        public Mail Attachment(FileStream stream, string name, string mediaType)
        {
            if (stream is null) return this;
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(stream, name, mediaType));
            return this;
        }

        public Mail Attachment(string fileName)
        {
            if (fileName.IsNullOrEmpty()) return this;
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(fileName));
            return this;
        }

        public Mail Attachment(string fileName, ContentType contentType)
        {
            if (fileName.IsNullOrEmpty()) return this;
            _attachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            _attachments.Add(new Attachment(fileName, contentType));
            return this;
        }

        public Mail Attachment(string fileName, string mediaType)
        {
            if (fileName.IsNullOrEmpty()) return this;
            _attachments ??= new List<Attachment>();
            _attachments.Add(new Attachment(fileName, mediaType));
            return this;
        }

        public Mail Attachment(List<Tuple<Stream, string>> attachments)
        {
            if (attachments.IsNullOrEmpty()) return this;
            _attachments ??= new List<Attachment>();
            attachments.ForEach(attachment => _attachments.Add(new Attachment(attachment.Item1, attachment.Item2)));
            return this;
        }

        public Mail Subject(string subject)
        {
            _subject = subject;
            return this;
        }

        public Mail Body(string body)
        {
            _body = body;
            return this;
        }

        public Mail IsBodyHtml(bool isBodyHtml)
        {
            _isBodyHtml = isBodyHtml;
            return this;
        }

        public Mail BodyEncoding(Encoding bodyEncoding)
        {
            _bodyEncoding = bodyEncoding;
            return this;
        }

        public Mail Priority(MailPriority mailPriority)
        {
            _mailPriority = mailPriority;
            return this;
        }

        public MailMessage CreateMail()
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