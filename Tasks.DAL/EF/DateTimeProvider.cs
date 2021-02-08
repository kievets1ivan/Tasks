using System;

namespace Tasks.DAL.EF
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentUTC { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetCurrentUTC
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
