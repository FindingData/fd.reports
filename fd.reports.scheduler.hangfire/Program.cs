using fd.infrastructure.core.CacheManager;
using fd.reports.core.IServices;
using fd.reports.core.Services;
using fd.reports.core.Utilities;
using fd.reports.job.HangfireJobs;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


// 配置自定义配置文件加载
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

SqlHelper.Initialize(builder.Configuration);
// 1️⃣ 注册报表依赖
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<ICacheService, MemoryCacheService>();
builder.Services.AddScoped<IExportStrategy, ExcelExportStrategy>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddSingleton<IExportStrategy, ExcelExportStrategy>();
builder.Services.AddSingleton<ReportService>();

// 注册 Job
builder.Services.AddTransient<HNBankLegerReportJob>();

GlobalConfiguration.Configuration.UseSQLiteStorage();

// 注册 Hangfire
builder.Services.AddHangfire(configuration => configuration
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage());

// 启动后台任务服务器
builder.Services.AddHangfireServer();

var app = builder.Build();

// 挂载 Dashboard
app.UseHangfireDashboard("/hangfire");

// 4️⃣ 注册定时任务：每季度第一天早8点执行
var jobManager = app.Services.GetRequiredService<IRecurringJobManager>();

// 注册每季度首日8点执行
jobManager.AddOrUpdate<HNBankLegerReportJob>(
    "HNBankLedgerQuarterlyJob",       // 任务唯一ID
    job => job.Run(),                 // 执行方法
    "0 8 1 1,4,7,10 *"                // CRON 表达式
);
Console.WriteLine($"当前环境: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
app.MapGet("/", () => "Hangfire Server is running.");

app.Run();
