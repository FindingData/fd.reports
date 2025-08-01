using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.schedule.Adapters
{

    public class QuartzJobAdapter<TJob> : IJob where TJob : class
    {
        private readonly TJob _job;

        public QuartzJobAdapter(TJob job)
        {
            _job = job;
        }

        public Task Execute(IJobExecutionContext context)
        {
            var runMethod = typeof(TJob).GetMethod("Execute");
            runMethod?.Invoke(_job, new object[] { DateTime.Now });
            return Task.CompletedTask;
        }
    }

}
