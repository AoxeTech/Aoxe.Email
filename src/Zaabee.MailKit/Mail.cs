using System.Collections.Generic;
using System.IO;
using System.Linq;
using MimeKit;

namespace Zaabee.MailKit
{
    public class Mail
    {
        private string _from;
        private string _subject;
        private string _body;
        private MessagePriority _messagePriority = MessagePriority.Normal;
        private List<string> _recipients;
        private List<string> _carbonCopies;
        private List<string> _blindCarbonCopies;
        private List<MimePart> _attachments;

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
        public Mail To(IEnumerable<string> recipients)
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
        public Mail To(params string[] recipients)
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
        public Mail Cc(IEnumerable<string> carbonCopies)
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
        public Mail Cc(params string[] carbonCopies)
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
        public Mail Bcc(IEnumerable<string> blindCarbonCopies)
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
        public Mail Bcc(params string[] blindCarbonCopies)
        {
            _blindCarbonCopies ??= new List<string>();
            _blindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        public Mail Attachment(Stream stream, string fileName)
        {
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

        internal MimeMessage CreateMail()
        {
            var mail = new MimeMessage();
            mail.From.Add(new MailboxAddress(_from, _from));
            mail.To.AddRange(_recipients?.Select(to => new MailboxAddress(to, to)).ToList());
            mail.Cc.AddRange(_carbonCopies?.Select(cc => new MailboxAddress(cc, cc)).ToList());
            mail.Bcc.AddRange(_blindCarbonCopies?.Select(bcc => new MailboxAddress(bcc, bcc)).ToList());

            mail.Subject = _subject;
            var multipart = new Multipart {new TextPart("html"){Text = _body}};
            _attachments?.ForEach(attachment =>  multipart.Add(attachment));
            mail.Body = multipart;
            mail.Priority = _messagePriority;
            
            return mail;
        }
    }
}