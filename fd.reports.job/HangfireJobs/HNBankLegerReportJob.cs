using fd.infrastructure.core.CacheManager;
using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.domain;
using fd.infrastructure.core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.Word;

namespace fd.reports.job.HangfireJobs
{
    public class HNBankLegerReportJob
    {
        private readonly IReportAppService _reportService;
        private readonly ICacheService _cacheService;

        public HNBankLegerReportJob(IReportAppService reportService, ICacheService cacheService)
        {
            _reportService = reportService;
            _cacheService = cacheService;
        }

        public async Task Run()
        {
            var today = DateTime.Now.Date;
            var lastQuarter = today.GetQuarterRange(-1);
            var cmd = new ExportReportCommand
            {
                report_type = ReportType.HNBankLedger,
                reprot_date = today,
                parameters = new Dictionary<string, object> {
                    { "report_start_date", lastQuarter.Start },
                    { "report_end_date", lastQuarter.End }
                }
            };
            var filePath = await _reportService.HandleExportCommand(cmd);            
            _cacheService.RPush("fd.reports.hnbank:pending_files", filePath);
            Console.WriteLine($"[Hangfire] 银行台账报表已导出: {filePath}");
        }
    }

}
