namespace Report.Extensibility
{
    public interface IReportGenerator
    {
        string GenerateReport(IReportInfo reportInfo);
    }
}