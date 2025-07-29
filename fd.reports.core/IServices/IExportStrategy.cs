using fd.reports.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fd.reports.core.IServices
{
    public interface IExportStrategy
    {
        Task<string> ExportAsync(DataTable data, ReportTask task);
    }

}
