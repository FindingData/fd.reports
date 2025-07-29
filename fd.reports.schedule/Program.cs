using Autofac;
using Autofac.Extensions.DependencyInjection;
using fd.infrastructure.core.CacheManager;
using fd.reports.core.IServices;
using fd.reports.core.Services;
using fd.reports.core.Utilities;
using fd.reports.domain;
using fd.reports.schedule;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Quartz;
using System.ComponentModel;

var builder = Host.CreateDefaultBuilder(args)
    //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureAppConfiguration((context, config) =>
    {
        var env = context.HostingEnvironment;
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        SqlHelper.Initialize(configuration);
        services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        // ✅ 注册报表核心服务
        services.AddMemoryCache();
        services.AddSingleton<ICacheService, MemoryCacheService>();
        
        services.AddScoped<IReportGenerator, SqlReportGenerator>();
        services.AddScoped<IExportStrategy, ExcelExportStrategy>();
        services.AddScoped<IReportAppService, ReportAppService>();
        
        services.AddSingleton<EmailSender>();
       
        services.AddQuartzJobs();
        //services.AddQuartzJobs();
    })
    //.ConfigureContainer<ContainerBuilder>(container =>
    //{
    //    container.RegisterType<MemoryCacheService>().As<ICacheService>().SingleInstance();
    //})
    ;

var host = builder.Build();
// ✅ 启动主服务（不立即 Run，让我们先判断）
await host.StartAsync();

var schedulerFactory = host.Services.GetRequiredService<ISchedulerFactory>();
var scheduler = await schedulerFactory.GetScheduler();

//var config = host.Services.GetRequiredService<IConfiguration>();
//var options = host.Services.GetRequiredService<IOptions<MailSettings>>();
//var baseDir = AppContext.BaseDirectory; // 指向 bin/Debug/net8.0/
//var exportDir = Path.Combine(baseDir, "exports");
//var reportPath = Path.Combine(exportDir, "report_2025-07-28.xlsx");
//EmailSender sender = new EmailSender(options);
//await sender.SendReportAsync("2917484924@qq.com", reportPath);
Console.WriteLine($"当前环境: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");

Console.WriteLine("按下回车键立即触发一次任务导出任务");
Console.ReadLine();
await scheduler.TriggerJob(new JobKey("ExportDailyReportJob"));
Console.WriteLine("按下回车键立即触发一次发送邮件任务");
Console.ReadLine();
await scheduler.TriggerJob(new JobKey("send-daily-report-job"));
Console.WriteLine("✅ 已触发任务执行。按任意键退出...");
Console.ReadLine();