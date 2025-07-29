namespace fd.reports.domain
{
    public class ReportTask
    {
        public ReportType ReportType { get; init; }
        public DateTime ReportDate { get; init; }
        public string OutputPath { get; init; }
    }

}
