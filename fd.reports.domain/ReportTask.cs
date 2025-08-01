namespace fd.reports.domain
{
    public class ReportTask
    {
        public ReportType report_type { get; init; }

  

        public Dictionary<string, object>? parameters { get; set; }

        public string output_path { get; init; }
    }

}
