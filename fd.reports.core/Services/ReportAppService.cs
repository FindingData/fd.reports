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
        private readonly IExportStrategy _exporter;

        public ReportAppService(IExportStrategy exporter)
        {
            _exporter = exporter;
        }

        public async Task<string> HandleExportCommand(ExportReportCommand cmd)
        {
            var task = new ReportTask
            {
                report_type = ReportType.ProjectProgress,               
                parameters = cmd.parameters,
                output_path = $"{cmd.report_type.ToString().ToLower()}_{cmd.reprot_date:yyyy-MM-dd}.xlsx",
            };
            var generator = CreateGenerator(cmd.report_type);
            var data = await generator.GenerateAsync(task);
            return await _exporter.ExportAsync(data, task);
        }

        /// <summary>
        /// 简单工厂，根据报表类型选择生成器
        /// </summary>
        private IReportGenerator CreateGenerator(ReportType type)
        {
            return type switch
            {
                ReportType.ProjectProgress =>
                    new SqlReportGenerator("sql/project_report.sql"),

                ReportType.HNBankLedger =>
                    new SqlReportGenerator("sql/hn_bank_ledger.sql"),

                _ => throw new NotSupportedException($"未支持的报表类型: {type}")
            };
        }

    }

}
