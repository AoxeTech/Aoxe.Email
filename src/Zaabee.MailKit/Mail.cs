using System.Collections.Generic;
using System.IO;
using System.Linq;
using MimeKit;
using Zaabee.Extensions;

namespace Zaabee.MailKit
{
    public class Mail
    {
        private string _from;
        public string GetFrom => _from;
        private string _subject;
        public string GetSubject => _subject;
        private string _body;
        public string GetBody => _body;
        private MessagePriority _messagePriority = MessagePriority.Normal;
        public MessagePriority GetMessagePriority => _messagePriority;
        private List<string> _recipients;
        public List<string> GetRecipients => _recipients;
        private List<string> _carbonCopies;
        public List<string> GetCarbonCopies => _carbonCopies;
        private List<string> _blindCarbonCopies;
        public List<string> GetBlindCarbonCopies => _blindCarbonCopies;
        private List<MimePart> _attachments;
        public List<MimePart> GetAttachments => _attachments;

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

        public Mail Attachment(Stream stream, string fileName)
        {
            if (stream is null) return this;
            _attachments ??= new List<MimePart>();
            var attachment = new MimePart
            {
                Content = new MimeContent(stream),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = fileName
            };
            _attachments.Add(attachment);
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

        public Mail Priority(MessagePriority messagePriority)
        {
            _messagePriority = messagePriority;
            return this;
        }

        public MimeMessage CreateMail()
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_from, _from));
            if (!_recipients.IsNullOrEmpty())
                mail.To.AddRange(_recipients.Select(to => new MailboxAddress(to, to)));
            if (!_carbonCopies.IsNullOrEmpty())
                mail.Cc.AddRange(_carbonCopies.Select(cc => new MailboxAddress(cc, cc)));
            if (!_blindCarbonCopies.IsNullOrEmpty())
                mail.Bcc.AddRange(_blindCarbonCopies.Select(bcc => new MailboxAddress(bcc, bcc)));

            mail.Subject = _subject;
            var multipart = new Multipart {new TextPart("html") {Text = _body}};
            _attachments?.ForEach(attachment => multipart.Add(attachment));
            mail.Body = multipart;
            mail.Priority = _messagePriority;

            return mail;
        }
    }
}