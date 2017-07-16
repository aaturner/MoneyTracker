using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace MoneyTracker.Extensions
{
    static class DateTimeExtensions
    {
        static GregorianCalendar _gc = new GregorianCalendar();
        private static DayOfWeek _endWeekDay = DayOfWeek.Friday;
        public static int GetWeekOfMonth(this DateTime time)  //this may not be reliable
        {
            DateTime first = new DateTime(time.Year, time.Month, 1);
            return time.GetWeekOfYear() - first.GetWeekOfYear() + 1;
        }

        static int GetWeekOfYear(this DateTime time)
        {
            return _gc.GetWeekOfYear(time, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static int GetWeeksInMonth(this DateTime time)
        {
            int retVal; 

            var firstDay = new DateTime(time.Year,time.Month,1);
            var day29 = firstDay.AddDays(29);
            var day30 = firstDay.AddDays(30);
            var day31 = firstDay.AddDays(31);

            if ((day29.Month == time.Month && day29.DayOfWeek == _endWeekDay)
                || (day30.Month == time.Month && day30.DayOfWeek == _endWeekDay)
                || (day31.Month == time.Month && day31.DayOfWeek == _endWeekDay))
            {
                retVal = 5;
            }
            else retVal = 4;

            return retVal;
        }
    }
}