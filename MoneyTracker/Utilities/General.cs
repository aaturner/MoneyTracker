using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;
using MoneyTracker.Models.ChangeEvents;
using MoneyTracker.Models.DataObjects;
using MoneyTracker.Extensions;
using MoneyTracker.Models.Enums;

namespace MoneyTracker.Utilities
{
    public class General
    {
        internal static List<Month> GetMonthList(DateTime start, DateTime end)
        {
            int years = end.Year - start.Year;
            int months = end.Month - start.Month;

            List<Month> retList = new List<Month>();

            int stopMonth = end.Month;
            if (years > 0) stopMonth = 12;
           
            //First Year
            for(int i = start.Month; i <= end.Month; i++)
            {
                retList.Add(new Month(i, start.Year));
            }

            //Middle Years
            if(years>2)
            {
                for(int i = start.Year +1; i < end.Year; i++)
                {
                    for(int j = 1; j ==13; j++)
                    {
                        retList.Add(new Month(j, i));
                    }
                }

            }

            //Last Year
            if(years>0)
            {
                for (int i = 1; i < end.Month + 1; i++)
                {
                    retList.Add(new Month(i, end.Year));
                }
            }

            return retList;
        }
        /// <summary>
        /// Returns the number of weeks in a given month using the WeekEnd/Start system setting 
        /// if system setting = Friday, returns # of fridays in the given month
        /// </summary>
        /// <param name="selectedMonth"></param>
        /// <param name="selectedYear"></param>
        /// <returns></returns>
        public static int WeeksInMonth(int selectedMonth, int selectedYear)
        {
            PrimaryContext db = new PrimaryContext();
            SystemSetting setting = db.SystemSettings.FirstOrDefault(x => x.Setting == Enums.SysSetting.WeekStartDay);
            int days = DateTime.DaysInMonth(selectedYear, selectedMonth);

            DateTime[] datesInMonth =
            {
                new DateTime(selectedYear, selectedMonth, 1),
                new DateTime(selectedYear, selectedMonth, days)
            };
            return datesInMonth.Count(x => x.DayOfWeek == setting.SettingDay);

        }

        internal static decimal GetAllocationWithChangeEventsByMonth(Allocation allocation, int selectedMonth, int selectedYear = 0)
        {
            PrimaryContext db = new PrimaryContext();
            if (selectedYear == 0) selectedYear = System.DateTime.Now.Year;
            decimal retVal = allocation.Amount;
            int days = DateTime.DaysInMonth(selectedYear, selectedMonth);
            DateTime endOfSelectedMonth = new DateTime(selectedYear, selectedMonth, days, 23,59,59);

            List<AllocationChange> changeEvents = db.ChangeEvents.OfType<AllocationChange>().Where(x => x.AllocationId == allocation.Id
                                                                                                        && x.EffectiveDateTime <= endOfSelectedMonth).ToList();
            changeEvents = changeEvents.OrderBy(change => change.EffectiveDateTime).ToList();

            //Apply change events
            foreach (var change in changeEvents)
            {
                retVal += GetChangeAmount(selectedMonth, selectedYear, retVal, change);
            }

            //Apply Recurance
            switch (allocation.Recurrence.RecurrenceFrequencyEnum)
            {
                case RecurrenceEnum.None:     //None
                    if (!(allocation.Recurrence.RecuranceStartDate.Month == selectedMonth
                        && allocation.Recurrence.RecuranceStartDate.Year == selectedYear))
                    {
                        retVal = decimal.Zero;
                    }
                    break;
                case RecurrenceEnum.Weekly:     //Weekly
                    DateTime date = new DateTime(selectedYear, selectedMonth, 1);
                    retVal = retVal * date.GetWeeksInMonth();
                    break;
                case RecurrenceEnum.Monthly:     //Monthly
                    //no change
                    break;
                case RecurrenceEnum.Yearly:     //Yearly
                    if (!(allocation.Recurrence.RecuranceStartDate.Month == selectedMonth))
                    {
                        retVal = decimal.Zero;
                    }
                    break;
            }

            return retVal;
        }

        private static decimal GetChangeAmount(int selectedMonth, int selectedYear, decimal sumingAmount, AllocationChange change)
        {
            decimal changeAmount = Decimal.Zero;
            switch (change.Recurrence.RecurrenceFrequencyEnum)
            {
                case RecurrenceEnum.None:
                    if (change.EffectiveDateTime.Month == selectedMonth
                        && change.EffectiveDateTime.Year == selectedYear)
                    {
                        changeAmount = CalcNewAmount(sumingAmount, change);
                    }
                    break;
                case RecurrenceEnum.Weekly:
                    changeAmount = CalcNewAmount(sumingAmount, change) *
                                   General.WeeksInMonth(selectedMonth, selectedYear);  //TODO: this calc is wrong, need to /#weeks
                    break;
                case RecurrenceEnum.Monthly:
                    changeAmount = CalcNewAmount(sumingAmount, change);
                    break;
                case RecurrenceEnum.Yearly:
                    if (change.EffectiveDateTime.Month == selectedMonth)
                    {
                        changeAmount = CalcNewAmount(sumingAmount, change);
                    }
                    break;

                
            }
            return changeAmount;

        }

        public static int GetFirstTransactionYear()
        {
            PrimaryContext db = new PrimaryContext();
            return db.Transactions.Min(x => x.TransactionDate).Year;
        }

        public static int YearsToDisplay()
        {
            PrimaryContext db = new PrimaryContext();
            int futureYearsDisplay = 1;
            if (db.SystemSettings.Any(x => x.Setting == Enums.SysSetting.NumYearsToDisplay))
            {
                futureYearsDisplay = Convert.ToInt16(db.SystemSettings.FirstOrDefault(x => x.Setting == Enums.SysSetting.NumYearsToDisplay).SettingValue);
            }
            return System.DateTime.Now.Year - GetFirstTransactionYear() + futureYearsDisplay;
        }
        private static decimal CalcNewAmount(decimal sumingAmount, AllocationChange change)
        {
            decimal delta = Decimal.Zero;
            if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.LumpSum))
            {
                delta = change.Amount;
            }
            if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.Percentage))
            {
                delta = sumingAmount * (1 + change.Amount);
            }

            return delta;
        }

        
    }
}