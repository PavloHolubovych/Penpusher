using System;

namespace Report.Extensibility
{
    public interface IDateTimeProvider
    {
        DateTime GetNow();
    }
}