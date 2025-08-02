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
    public class ReportService : IReportService
    {
        private readonly IExportStrategy _exporter;
        private readonly IReportGenerator _generator;
        public ReportService(IExportStrategy exporter, IReportGenerator generator)
        {
            _exporter = exporter;
            _generator = generator; 
        }

        public async Task<string> HandleExportCommand(ExportReportCommand cmd)
        {
            var task = new ReportTask
            {
                report_type = cmd.report_type,
                parameters = cmd.parameters,
                output_path = $"{cmd.report_type.ToString().ToLower()}_{cmd.reprot_date:yyyy-MM-dd}.xlsx",
            };
            var data = await _generator.GenerateAsync(cmd.sql_file, cmd.parameters);
            return await _exporter.ExportAsync(data, task);
        }

    }

}
