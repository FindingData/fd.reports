using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.job.HangfireJobs
{
    public class HangfireReportJob
    {
        public void Execute()
        {
            Console.WriteLine("Hangfire Job Executed");
        }
    }

}
