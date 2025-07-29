using fd.reports.core.Commands;
using fd.reports.core.IServices;
using fd.reports.core.Services;
using Quartz;

namespace fd.reports.job
{
    public class ExportDailyReportJob : IJob
    {
        private readonly IReportAppService _appService;

        public ExportDailyReportJob(IReportAppService appService)
        {
            _appService = appService;
        }


        public async Task Execute(IJobExecutionContext context)
        {
            var today = DateTime.Today.AddDays(-1); // 导出昨日数据
            var cmd = new ExportReportCommand { ReportDate = today };
            var path = await _appService.HandleExportCommand(cmd);
            Console.WriteLine($"✅ 导出成功: {path}");
        }
    }

}
