using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using Microsoft.Data.Sqlite;


namespace fd.reports.core.Utilities
{
    public static class SqliteHelper
    {
        public static bool ValidateDatabase(string dbPath)
        {
            try
            {
                // 1. 文件是否存在
                if (!File.Exists(dbPath))
                {
                    Console.WriteLine($"❌ 数据库文件不存在: {dbPath}");
                    return false;
                }

                // 2. 打开连接测试
                var connStr = $"Data Source={dbPath};Cache=Shared";
                using var conn = new SqliteConnection(connStr);
                conn.Open();

                // 3. 测试是否能执行简单 SQL
                using var cmd = conn.CreateCommand();
                cmd.CommandText = "PRAGMA integrity_check;";
                var result = cmd.ExecuteScalar()?.ToString();

                if (result != "ok")
                {
                    Console.WriteLine($"⚠️ 数据库完整性检查异常: {result}");
                    return false;
                }

                Console.WriteLine("✅ SQLite 数据库可访问且完整");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ SQLite 验证失败: {ex.Message}");
                return false;
            }
        }
    }

}
