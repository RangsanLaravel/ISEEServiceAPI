using ISEEService.DataContract;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace ISEEService.BusinessLogic
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        public MailService(MailSettings mailSettings)
        {
            _mailSettings = mailSettings;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            if (mailRequest.Cc != null && mailRequest.Cc.Count > 0)
            {
                foreach (var item in mailRequest.Cc)
                {
                    email.Cc.Add(MailboxAddress.Parse(item));
                }
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                //byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.FileData.Length > 0)
                    {
                        //using (var ms = new MemoryStream())
                        //{
                        //    file.CopyTo(ms);
                        //    fileBytes = ms.ToArray();
                        //}
                        builder.Attachments.Add(file.FileName, file.FileData, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
