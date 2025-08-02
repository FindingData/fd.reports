namespace fd.reports.domain
{
    public class ReportTask
    {
        public string report_type { get; init; }
  
        public Dictionary<string, object>? parameters { get; set; }

        public List<string> recipients { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public string output_path { get; init; }
    }

}
