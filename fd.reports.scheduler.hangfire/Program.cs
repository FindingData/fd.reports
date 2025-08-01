using fd.reports.core.Utilities;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);


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

app.MapGet("/", () => "Hangfire Server is running.");

app.Run();
