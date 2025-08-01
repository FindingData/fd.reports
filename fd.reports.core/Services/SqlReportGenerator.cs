using fd.reports.core.IServices;
using fd.reports.core.Utilities;
using fd.reports.domain;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.Services
{
    public class SqlReportGenerator : IReportGenerator
    {
        private readonly string _sqlFile;

        public SqlReportGenerator(string sqlFile)
        {
            _sqlFile = sqlFile;
        }

        public async Task<DataTable> GenerateAsync(ReportTask task)
        {
            // 1. 读取 SQL 模板
            var sql = File.ReadAllText(_sqlFile);
            // 2. 根据任务类型决定参数
            var parameters = BuildParameters(task);
            // 3. 执行 SQL 并返回 DataTable
            return await SqlHelper.QueryAsyncV2(sql,parameters); // 可返回 DataTable
        }

        /// <summary>
        /// 根据报表任务构建参数
        /// </summary>
        private OracleParameter[] BuildParameters(ReportTask task)
        {
            return task.report_type switch
            {
                ReportType.ProjectProgress => new OracleParameter[]
                {
                      new("start_date", OracleDbType.Date) {
                      Value = task.parameters != null && task.parameters.TryGetValue("report_start_date", out var v) ? v : DateTime.Now 
                      },
                },
                ReportType.HNBankLedger => new OracleParameter[]
                {
                     new("start_date", OracleDbType.Date) {
                     Value = task.parameters != null && task.parameters.TryGetValue("report_start_date", out var v1) ? v1 : DateTime.Now
                     },
                     new("end_date", OracleDbType.Date) {
                       Value = task.parameters != null && task.parameters.TryGetValue("report_end_date", out var v2) ? v2 : DateTime.Now
                     }
                },
                _ => Array.Empty<OracleParameter>()
            };
        }
    }

}
