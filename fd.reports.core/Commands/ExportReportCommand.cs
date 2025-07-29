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
        public ReportType ReportType { get; set; } = ReportType.ProjectProgress;

        public DateTime ReportDate { get; set; }

        public ExportReportCommand()
        {
            ReportDate = DateTime.Today;
        }
    }
}
