using System.Collections.Generic;
using MoneyTracker.Utilities;

namespace MoneyTracker.AppModels
{
    public class ForecastRow
    {
        public ForecastRow()
        {
        }

        public ForecastRow(Enums.TableRowType rowType, string accountName, string monthCurrent, string month1, string month2, string month3,
            string month4, string month5, string month6, string year1, string year2, string year3)
        {
            AccountName = accountName;
            MonthCurrent = monthCurrent;
            Month1 = month1;
            Month2 = month2;
            Month3 = month3;
            Month4 = month4;
            Month5 = month5;
            Month6 = month6;

            Year1 = year1;
            Year2 = year2;
            Year3 = year3;
        }

        public Enums.TableRowType RowType { get; set; }

        public string AccountType { get; set; }
        public string AccountName { get; set; }

        public string MonthCurrent { get; set; }
        public string Month1 { get; set; }
        public string Month2 { get; set; }
        public string Month3 { get; set; }
        public string Month4 { get; set; }
        public string Month5 { get; set; }
        public string Month6 { get; set; }

        public string Year1 { get; set; }
        public string Year2 { get; set; }
        public string Year3 { get; set; }


    }
}