using fd.infrastructure.core.CacheManager;
using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.core.Services;
using fd.reports.domain;
using Microsoft.Extensions.Options;
using Quartz;
using System.Text.Json;

namespace fd.reports.job.QuartzJobs
{
    public class ExportDailyReportJob : ReportJobBase, IJob
    {
        protected override string ReportType => "daily_report";


        public ExportDailyReportJob(IReportService reportService,
            ICacheService cacheService,
            IOptions<List<ReportTemplate>> templates,
            IOptions<List<PendingMail>> pendingMails) : base(reportService, cacheService, templates,pendingMails)
        {

        }
    }

}
