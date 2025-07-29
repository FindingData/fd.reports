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
        public async Task<DataTable> GenerateAsync(ReportTask task)
        {
            var sql = File.ReadAllText("sql/project_report.sql");
            //var parameters = new OracleParameter[]
            //{
            //    new("start_date", OracleDbType.Date) { Value = task.ReportDate.Date },
            //    new("end_date", OracleDbType.Date) { Value = task.ReportDate.Date.AddDays(1).AddTicks(-1) }
            //};

            return await SqlHelper.QueryAsync(sql); // 可返回 DataTable
        }
    }

}
