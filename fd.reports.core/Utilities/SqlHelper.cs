using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.Utilities
{
    public static class SqlHelper
    {
        private static string? _connectionString;

        public static void Initialize(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("oaDb");
        }

        public static async Task<DataTable> QueryAsync(string sql, params OracleParameter[] parameters)
        {
            if (_connectionString == null)
                throw new InvalidOperationException("SqlHelper not initialized.");

            using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new OracleCommand(sql, conn);            
            cmd.Parameters.AddRange(parameters);           
            using var reader = await cmd.ExecuteReaderAsync();

            var table = new DataTable();
            table.Load(reader);

            return table;
        }

        public static async Task<DataTable> QueryAsyncV2(string sql, params OracleParameter[] parameters)
        {
            if (_connectionString == null)
                throw new InvalidOperationException("SqlHelper not initialized.");

            using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();

            using var cmd = new OracleCommand(sql, conn);
            cmd.BindByName = true;
            cmd.Parameters.AddRange(parameters);
            var adapter = new OracleDataAdapter(cmd);           
            var table = new DataTable();
            adapter.Fill(table);

            return table;
        }
    }
}
