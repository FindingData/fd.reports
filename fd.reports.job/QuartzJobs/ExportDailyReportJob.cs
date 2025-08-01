using fd.infrastructure.core.CacheManager;
using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.core.Services;
using Quartz;

namespace fd.reports.job.QuartzJobs
{
    public class ExportDailyReportJob : IJob
    {
        private readonly IReportAppService _appService;
        private readonly ICacheService _cacheService;

        public ExportDailyReportJob(IReportAppService appService, ICacheService cacheService)
        {
            _appService = appService;
            _cacheService = cacheService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var today = DateTime.Today.AddDays(-1); // 导出昨日数据
            var cmd = new ExportReportCommand { ReportDate = today };
            var path = await _appService.HandleExportCommand(cmd);
            _cacheService.RPush("fd.reports:pending_files", path);
            Console.WriteLine($"✅ 导出成功: {path}");
        }
    }

}
