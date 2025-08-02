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
        public async Task<DataTable> GenerateAsync(string sqlFile, Dictionary<string, object> parameters)
        {
            // 1. 读取 SQL 模板
            var sql = File.ReadAllText(sqlFile);
            // 2. 根据任务类型决定参数
            var oracleParams = ToOracleParameters(parameters);
            // 3. 执行 SQL 并返回 DataTable
            return await SqlHelper.QueryAsyncV2(sql, oracleParams); // 可返回 DataTable
        }

        public OracleParameter[] ToOracleParameters(Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return Array.Empty<OracleParameter>();

            return parameters.Select(kv =>
            {
                var paramName = kv.Key.StartsWith(":") ? kv.Key : $":{kv.Key}";
                var value = kv.Value ?? DBNull.Value;

                var oracleType = GetOracleDbType(value);

                return new OracleParameter(paramName, oracleType)
                {
                    Value = value
                };
            }).ToArray();
        }

        private OracleDbType GetOracleDbType(object value)
        {
            return value switch
            {
                int => OracleDbType.Int32,
                long => OracleDbType.Int64,
                decimal => OracleDbType.Decimal,
                double => OracleDbType.Double,
                DateTime => OracleDbType.Date,
                bool => OracleDbType.Int16, // Oracle没有bool，用0/1
                _ => OracleDbType.Varchar2
            };
        }


    }

}
