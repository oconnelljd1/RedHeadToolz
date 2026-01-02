using System;

namespace RedHeadToolz.Utils
{
    public static class DateTimeExtensions
    {
        public static double GetTotalSeconds(this DateTime dateTime)
        {
            return dateTime.Subtract(DateTime.MinValue).TotalSeconds;
        }

        public static DateTime GetYesterday(this DateTime dateTime)
        {
            var now = DateTime.Now;
            return now.AddDays(-1);
        }

        public static string FormatMDY(this DateTime dateTime)
        {
            return $"{dateTime.Month}-{dateTime.Day}-{dateTime.Year}";
        }
    }
}