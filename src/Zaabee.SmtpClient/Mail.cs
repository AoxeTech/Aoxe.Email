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
        public string? GetFrom { get; private set; }

        public string? GetSubject { get; private set; }

        public bool GetIsBodyHtml { get; private set; } = true;

        public Encoding GetBodyEncoding { get; private set; } = Encoding.UTF8;

        public string? GetBody { get; private set; }

        public MailPriority GetMailPriority { get; private set; } = MailPriority.Normal;

        public List<string>? GetRecipients { get; private set; }

        public List<string>? GetCarbonCopies { get; private set; }

        public List<string>? GetBlindCarbonCopies { get; private set; }

        public List<Attachment>? GetAttachments { get; private set; }

        /// <summary>
        /// From address
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        public Mail From(string from)
        {
            GetFrom = from;
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
            GetRecipients ??= new List<string>();
            GetRecipients.AddRange(recipients);
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
            GetRecipients ??= new List<string>();
            GetRecipients.AddRange(recipients);
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
            GetCarbonCopies ??= new List<string>();
            GetCarbonCopies.AddRange(carbonCopies);
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
            GetCarbonCopies ??= new List<string>();
            GetCarbonCopies.AddRange(carbonCopies);
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
            GetBlindCarbonCopies ??= new List<string>();
            GetBlindCarbonCopies.AddRange(blindCarbonCopies);
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
            GetBlindCarbonCopies ??= new List<string>();
            GetBlindCarbonCopies.AddRange(blindCarbonCopies);
            return this;
        }

        public Mail Attachment(FileStream stream, ContentType? contentType = null)
        {
            if (stream is null) return this;
            GetAttachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            GetAttachments.Add(new Attachment(stream, contentType));
            return this;
        }

        public Mail Attachment(Stream stream, string name)
        {
            if (stream is null) return this;
            GetAttachments ??= new List<Attachment>();
            GetAttachments.Add(new Attachment(stream, name));
            return this;
        }

        public Mail Attachment(FileStream stream, string name, string mediaType)
        {
            if (stream is null) return this;
            GetAttachments ??= new List<Attachment>();
            GetAttachments.Add(new Attachment(stream, name, mediaType));
            return this;
        }

        public Mail Attachment(string fileName)
        {
            if (fileName.IsNullOrEmpty()) return this;
            GetAttachments ??= new List<Attachment>();
            GetAttachments.Add(new Attachment(fileName));
            return this;
        }

        public Mail Attachment(string fileName, ContentType contentType)
        {
            if (fileName.IsNullOrEmpty()) return this;
            GetAttachments ??= new List<Attachment>();
            contentType ??= new ContentType(MediaTypeNames.Text.Plain);
            GetAttachments.Add(new Attachment(fileName, contentType));
            return this;
        }

        public Mail Attachment(string fileName, string mediaType)
        {
            if (fileName.IsNullOrEmpty()) return this;
            GetAttachments ??= new List<Attachment>();
            GetAttachments.Add(new Attachment(fileName, mediaType));
            return this;
        }

        public Mail Attachment(List<Tuple<Stream, string>> attachments)
        {
            if (attachments.IsNullOrEmpty()) return this;
            GetAttachments ??= new List<Attachment>();
            attachments.ForEach(attachment => GetAttachments.Add(new Attachment(attachment.Item1, attachment.Item2)));
            return this;
        }

        public Mail Subject(string subject)
        {
            GetSubject = subject;
            return this;
        }

        public Mail Body(string body)
        {
            GetBody = body;
            return this;
        }

        public Mail IsBodyHtml(bool isBodyHtml)
        {
            GetIsBodyHtml = isBodyHtml;
            return this;
        }

        public Mail BodyEncoding(Encoding bodyEncoding)
        {
            GetBodyEncoding = bodyEncoding;
            return this;
        }

        public Mail Priority(MailPriority mailPriority)
        {
            GetMailPriority = mailPriority;
            return this;
        }

        public MailMessage CreateMail()
        {
            var mail = new MailMessage
            {
                From = new MailAddress(GetFrom),
                Subject = GetSubject,
                IsBodyHtml = GetIsBodyHtml,
                BodyEncoding = GetBodyEncoding,
                Body = GetBody,
                Priority = GetMailPriority
            };

            GetAttachments?.ForEach(attachment => mail.Attachments.Add(attachment));
            GetRecipients?.ForEach(to => mail.To.Add(new MailAddress(to)));
            GetCarbonCopies?.ForEach(cc => mail.CC.Add(new MailAddress(cc)));
            GetBlindCarbonCopies?.ForEach(bcc => mail.Bcc.Add(new MailAddress(bcc)));

            return mail;
        }
    }
}