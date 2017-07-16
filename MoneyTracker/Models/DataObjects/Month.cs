using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models.DataObjects
{
    public class Month
    {
        public Month(int month, int year)
        {
            MonthNumber = month;
            Year = year;
            MonthStart = new DateTime(year, month, 1, 0, 0, 0);
        }
        public int MonthNumber { get; set; }
        public int Year { get; set; }
        public DateTime MonthStart { get; set; }
    }
}