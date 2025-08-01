using ClosedXML.Excel;
using fd.reports.core.IServices;
using fd.reports.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.Services
{
    public class ExcelExportStrategy : IExportStrategy
    {
        public async Task<string> ExportAsync(DataTable data, ReportTask task)
        {
            var fileName = task.output_path;
            var filePath = Path.Combine("exports", fileName);

            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("报表");
            sheet.Cell(1, 1).InsertTable(data);

            workbook.SaveAs(filePath);
            return filePath;
        }
    }

}
