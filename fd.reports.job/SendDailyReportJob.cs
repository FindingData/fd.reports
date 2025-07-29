using fd.infrastructure.core.CacheManager;
using fd.reports.core.Utilities;
using fd.reports.domain;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.job
{
    public class SendDailyReportJob : IJob
    {
        private readonly EmailSender _emailSender;
        private readonly ICacheService _cacheService;
        private readonly MailSettings _mailSettings;
        public SendDailyReportJob(EmailSender emailSender, ICacheService cacheService,
            IOptions<MailSettings> mailOptions)
        {
            _emailSender = emailSender;
            _cacheService = cacheService;
            _mailSettings = mailOptions.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var fileToSend = _cacheService.ListDequeue("fd.reports:pending_files");
            await _emailSender.SendReportAsync(_mailSettings.ToEmail, fileToSend.ToString());
        }
    }

}
