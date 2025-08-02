using fd.reports.core.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.IServices
{
    public interface IReportService
    {
        /// <summary>
        /// 执行报表导出任务
        /// </summary>
        /// <param name="command">导出指令</param>
        /// <returns>返回导出文件路径</returns>
        Task<string> HandleExportCommand(ExportReportCommand command);
    }
}
