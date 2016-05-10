using System;
using Report.Extensibility;

namespace Report.Service
{
    public class ReportInfo : IReportInfo
    {
        public ReportInfo(DateTime timeStamp, string name, int sum)
        {
            TimeStamp = timeStamp;
            Name = name;
            Sum = sum;
        }

        public DateTime TimeStamp { get; }

        public string Name { get; }

        public int Sum { get; }
    }
}