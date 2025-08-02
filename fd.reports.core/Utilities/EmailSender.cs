using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fd.reports.domain;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace fd.reports.core.Utilities
{  
    public class EmailSender
    {
        private readonly MailSettings _settings;

        public EmailSender(IOptions<MailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendReportAsync(IEnumerable<string> recipients, string filePath, string? subject,string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.from_name, _settings.from_email));
            foreach (var recipient in recipients)
                message.To.Add(new MailboxAddress(recipient, recipient));

            message.Subject = subject;

            var builder = new BodyBuilder { TextBody = body };
            builder.Attachments.Add(filePath);
            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.smtp_server, _settings.port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.user, _settings.password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }

}
