using fd.reports.core.IServices;
using fd.reports.core.Services;
using fd.reports.core.Utilities;
using fd.reports.schedule;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Quartz;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        SqlHelper.Initialize(configuration);

        // ✅ 注册报表核心服务
        services.AddScoped<IReportGenerator, SqlReportGenerator>();
        services.AddScoped<IExportStrategy, ExcelExportStrategy>();
        services.AddScoped<IReportAppService, ReportAppService>();
        services.AddQuartzJobs();

        //services.AddQuartzJobs();
    });

var host = builder.Build();
// ✅ 启动主服务（不立即 Run，让我们先判断）
await host.StartAsync();

var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

Console.WriteLine("按下回车键立即触发一次任务");
Console.ReadLine();

await scheduler.TriggerJob(new JobKey("ExportDailyReportJob"));

Console.WriteLine("✅ 已触发任务执行。按任意键退出...");
Console.ReadLine();