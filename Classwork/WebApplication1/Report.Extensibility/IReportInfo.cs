using System;

namespace Report.Extensibility
{
    public interface IReportInfo
    {
        DateTime TimeStamp { get; }

        string Name { get; }

        int Sum { get; }
    }
}