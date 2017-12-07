using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Utilities;

namespace MoneyTracker.Extensions
{
    static class DateTimeExtensions
    {
        static GregorianCalendar _gc = new GregorianCalendar();
        //private static DayOfWeek _endWeekDay = DayOfWeek.Friday; Using system setting
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
            PrimaryContext db = new PrimaryContext();
            DayOfWeek settingDay;
            if (db.SystemSettings.Any(x => x.Setting == Enums.SysSetting.WeekStartDay))
            {
                settingDay = (DayOfWeek)Convert.ToInt16(db.SystemSettings.FirstOrDefault(x => x.Setting == Enums.SysSetting.WeekStartDay).SettingValue);
            }
            else
            {
                db.SystemSettings.Add(new SystemSetting() {Setting = Enums.SysSetting.WeekStartDay, SettingInt = (int)DayOfWeek.Monday});
                db.SaveChanges();
                settingDay = DayOfWeek.Monday;
            }
            
            int daysInMonth = _gc.GetDaysInMonth(time.Year, time.Month);
            DayOfWeek firstDay = _gc.GetDayOfWeek(new DateTime(time.Year,time.Month,1));
            DateTime lastDay = new DateTime(time.Year, time.Month,daysInMonth);
            int count = (int) Math.Floor((decimal)(daysInMonth / 7));
            int remainder = (int) (daysInMonth % 7);
            int daysToLastDay = (int) (lastDay.DayOfWeek - settingDay);         //index setting day to last day of the month
            if (daysToLastDay < 0) daysToLastDay += 7;                          //account for setting Day after last day of week
            if (remainder >= daysToLastDay) count++;                            

            return count;
        }


        //These could use refinement
        public static int GetYears(this TimeSpan timespan)
        {
            return (int)(timespan.Days / 365.2425);
        }
        public static int GetMonths(this TimeSpan timespan)
        {
            return (int)(timespan.Days / 30.436875);
        }
        //public static int GetDaysInMonth(this DateTime time)
        //{
        //    int retVal;

        //    var firstDay = new DateTime(time.Year, time.Month, 1);
        //    var day29 = firstDay.AddDays(29);
        //    var day30 = firstDay.AddDays(30);
        //    var day31 = firstDay.AddDays(31);

        //    if ((day29.Month == time.Month && day29.DayOfWeek == _endWeekDay)
        //        || (day30.Month == time.Month && day30.DayOfWeek == _endWeekDay)
        //        || (day31.Month == time.Month && day31.DayOfWeek == _endWeekDay))
        //    {
        //        retVal = 5;
        //    }
        //    else retVal = 4;

        //    return retVal;
        //}
    }
}