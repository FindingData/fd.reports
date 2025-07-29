using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Quartz.Simpl;
using fd.reports.job;

namespace fd.reports.schedule
{
    public static class QuartzConfig
    {
        public static void AddQuartzJobs(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // 注册定时报表导出任务
                var jobKey = new JobKey("ExportDailyReportJob");

                q.AddJob<ExportDailyReportJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("ExportDailyReportTrigger")
                    .WithCronSchedule("0 0 1 * * ?") // 每天凌晨 1 点执行
                );
            });

            services.AddQuartzHostedService(opt =>
            {
                opt.WaitForJobsToComplete = true;
            });
        }
    }
}
   