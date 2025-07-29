using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.Services
{
    public class ReportAppService : IReportAppService
    {
        private readonly IReportGenerator _generator;
        private readonly IExportStrategy _exporter;

        public ReportAppService(IReportGenerator generator, IExportStrategy exporter)
        {
            _generator = generator;
            _exporter = exporter;
        }

        public async Task<string> HandleExportCommand(ExportReportCommand cmd)
        {
            var task = new ReportTask
            {
                ReportType = ReportType.ProjectProgress,
                ReportDate = cmd.ReportDate,
                OutputPath = $"exports/report_{cmd.ReportDate:yyyy-MM-dd}.xlsx"
            };

            var data = await _generator.GenerateAsync(task);
            return await _exporter.ExportAsync(data, task);
        }
    }

}
