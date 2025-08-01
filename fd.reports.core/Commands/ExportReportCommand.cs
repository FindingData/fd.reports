using fd.reports.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.Commands
{
    public class ExportReportCommand
    {
        public ReportType report_type { get; set; } = ReportType.ProjectProgress;

        public DateTime reprot_date { get; set; }

        public Dictionary<string, object>? parameters { get; set; }

        public ExportReportCommand()
        {
            reprot_date = DateTime.Today;
        }
    }
}
