using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.domain
{
    public class MailSettings
    {
        public string smtp_server { get; set; }
        public int port { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public string from_name { get; set; }
        public string from_email { get; set; }

    }
}
