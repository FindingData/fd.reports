using fd.infrastructure.core.CacheManager;
using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.domain;
using fd.infrastructure.core.Extensions;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.Json;

namespace fd.reports.job
{
    public abstract class ReportJobBase : IJob
    {
        protected readonly IReportService _reportService;
        protected readonly ICacheService _cacheService;
        protected readonly List<ReportTemplate> _templates;
        protected readonly IList<PendingMail> _pendingMails;

        protected ReportJobBase(
            IReportService reportService,
            ICacheService cacheService,
            IOptions<List<ReportTemplate>> templates,
            IOptions<List<PendingMail>> pendingMails)
        {
            _reportService = reportService;
            _cacheService = cacheService;
            _templates = templates.Value;
            _pendingMails = pendingMails.Value;
        }

        protected abstract string ReportType { get; }

        public async Task Execute(IJobExecutionContext context)
        {
            var template = _templates.FirstOrDefault(t => t.report_type == ReportType);
            if (template == null)
            {
                Console.WriteLine($"[Job] 未找到报表模板: {ReportType}");
                return;
            }

            var parameters = ResolveParameters(template.default_parameters);
            parameters = MergeParameters(parameters);

            var cmd = new ExportReportCommand
            {
                report_type = ReportType,
                reprot_date = DateTime.Now.Date,
                parameters = parameters,
                sql_file = template.sql_file,
            };

            var filePath = await _reportService.HandleExportCommand(cmd);

            // 后续交给子类处理，例如入队 PendingMail
            AfterExport(filePath);
        }

        protected virtual Dictionary<string, object> MergeParameters(Dictionary<string, object> parameters)
            => parameters;

        protected virtual void AfterExport(string filePath)
        {
            // 读取对应 PendingMail 配置
            var pendingMail = _pendingMails.FirstOrDefault(m => m.report_type == ReportType);
            if (pendingMail == null)
            {
                Console.WriteLine($"[Job] 未找到邮件配置: {ReportType}");
                return;
            }
            pendingMail.file_path = filePath;
            var key = $"fd.reports.{ReportType}:pending_files";
            _cacheService.RPush(key, JsonSerializer.Serialize(pendingMail));
            Console.WriteLine($"[Job] 报表已导出并推入发送队列: {filePath}");
        }

        protected virtual Dictionary<string, object> ResolveParameters(Dictionary<string, string> rawParams)
        {
            var dict = new Dictionary<string, object>();
            foreach (var kv in rawParams)
            {
                dict[kv.Key] = kv.Value switch
                {
                    "@yesterday" => DateTime.Now.AddDays(-2),
                    "@today" => DateTime.Now.Date,
                    "@daily_start_date" => DateTime.Now.AddYears(-1),
                    "@last_quarter_start" => DateTime.Now.GetQuarterRange(-1).Start,
                    "@last_quarter_end" => DateTime.Now.GetQuarterRange(-1).End,
                    _ => kv.Value
                };
            }
            return dict;
        }
    }
}
