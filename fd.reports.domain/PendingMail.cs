using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.domain
{
    public class PendingMail
    {
        public string report_type { get; set; }    // 报表类型，如 daily_report
        public string file_path { get; set; }      // 文件路径
        public string[] recipients { get; set; }   // 收件人列表
        public string subject { get; set; }        // 邮件主题
        public string body { get; set; }           // 邮件正文
    }
}
