using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.domain
{
    public enum ReportType
    {
        /// <summary>
        /// 项目进度报表（主SQL）
        /// </summary>
        ProjectProgress = 1,
        HNBankLedger = 2,
        // 可拓展其他类型：
        // TaskSummary = 2,
        // BankReport = 3,
        // EvaluationFee = 4
    }
}
