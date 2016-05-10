using System;
using Report.Extensibility;

namespace Report.Service
{
    public class ReportGenerator : IReportGenerator
    {
        public string GenerateReport(IReportInfo reportInfo)
        {
            return String.Format("Name: {0}, When: {1}, Sum: {2}", reportInfo.Name, reportInfo.TimeStamp, reportInfo.Sum);
        }
    }
}