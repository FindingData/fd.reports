using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.domain
{
    public class ReportTemplate
    {
        public string report_type { get; set; }
        public string sql_file { get; set; }
        public Dictionary<string, string> default_parameters { get; set; }
    }
}
