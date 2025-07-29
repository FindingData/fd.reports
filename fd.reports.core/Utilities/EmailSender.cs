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

        public async Task SendReportAsync(string toEmail, string filePath, string? subject = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.FromName, _settings.FromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject ?? "📊 报表已生成";

            var builder = new BodyBuilder
            {
                TextBody = "您好，\n\n请查收附件。\n\n-- 报表系统"
            };

            builder.Attachments.Add(filePath);

            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_settings.SmtpServer, _settings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_settings.User, _settings.Password);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }

}
