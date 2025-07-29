using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.domain
{
    public class MailSettings
    {
        public string SmtpServer { get; set; } = default!;
        public int Port { get; set; }
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FromName { get; set; } = default!;
        public string FromEmail { get; set; } = default!;

        public string ToEmail { get; set; } = default!;
    }
}
