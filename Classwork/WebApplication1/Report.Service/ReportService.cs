using Report.Extensibility;

namespace Report.Service
{
    public class ReportService : IReportService
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IReportGenerator reportGenerator;

        public ReportService(IDateTimeProvider dateTimeProvider, IReportGenerator reportGenerator)
        {
            this.dateTimeProvider = dateTimeProvider;
            this.reportGenerator = reportGenerator;
        }

        public string GetReport()
        {
            IReportInfo reportInfo = new ReportInfo(dateTimeProvider.GetNow(), "SomeName", 123);
            return reportGenerator.GenerateReport(reportInfo);
        }
    }
}