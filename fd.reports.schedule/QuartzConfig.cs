using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Simpl;
using fd.reports.job.QuartzJobs;
using fd.reports.schedule.Adapters;

namespace fd.reports.schedule
{
    public static class QuartzConfig
    {
        public static void AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();

                // 注册定时报表导出任务
                var jobKey = new JobKey("ExportDailyReportJob");

                q.AddJob<ExportDailyReportJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("ExportDailyReportTrigger")
                    .WithCronSchedule("0 0 1 ? * MON-FRI") // 每天凌晨 1 点执行
                );

                q.ScheduleJob<SendDailyReportJob>(trigger => trigger
                  .WithIdentity("send-daily-report-job")
                   .WithCronSchedule("0 0 8 ? * MON-FRI") // 每周一到五 8 点
                  );

            });

            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });
        }
    }
}
   