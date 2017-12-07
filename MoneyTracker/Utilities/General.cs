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
            decimal changeAmount = GetTotalChangeAmountForMonthDecimal(allocation,selectedMonth,selectedYear);

            switch (allocation.Recurrence.RecurrenceFrequencyEnum)
            {
                case RecurrenceEnum.None:     //None
                    if ((allocation.Recurrence.RecuranceStartDate.Month == selectedMonth
                        && allocation.Recurrence.RecuranceStartDate.Year == selectedYear))
                    {
                        retVal = retVal + changeAmount;
                    }
                    else
                    {
                        retVal = decimal.Zero;
                    }
                    break;
                case RecurrenceEnum.Weekly:     //Weekly
                    DateTime date = new DateTime(selectedYear, selectedMonth, 1);
                    int weeks = date.GetWeeksInMonth();
                    retVal = (retVal + changeAmount) * weeks;
                    break;
                case RecurrenceEnum.Monthly:     //Monthly
                    retVal = retVal + changeAmount;
                    break;
                case RecurrenceEnum.Yearly:     //Yearly
                    if ((allocation.Recurrence.RecuranceStartDate.Month == selectedMonth))
                    {
                        retVal = retVal + changeAmount;
                    }
                    else
                    { 
                        retVal = decimal.Zero;
                    }
                    break;
            }

            return retVal;
        }
        
        /// <summary>
        /// returns the sum of change amount deltas, not including the initial amount of the allocation
        /// Change events are applied once per month
        /// </summary>
        /// <param name="allocation"></param>
        /// <param name="selectedMonth"></param>
        /// <param name="selectedYear"></param>
        /// <returns></returns>
        private static decimal GetTotalChangeAmountForMonthDecimal(Allocation allocation, int selectedMonth, int selectedYear)
        {
            decimal retVal = decimal.Zero;
            PrimaryContext db = new PrimaryContext();
            decimal summingAmount = allocation.Amount;
            int days = DateTime.DaysInMonth(selectedYear, selectedMonth);
            DateTime endOfSelectedMonth = new DateTime(selectedYear, selectedMonth, days, 23, 59, 59);
            DateTime beginOfSelectedMonth = new DateTime(selectedYear, selectedMonth, 1, 00, 00, 01);

            List<AllocationChange> changeEvents = db.ChangeEvents.OfType<AllocationChange>().
                Where(x => x.AllocationId == allocation.Id
                && x.EffectiveDateTime <= endOfSelectedMonth).ToList();
            //Get this working List<ChangeEvent> changeEvents = allocation.ChangeEvents.Where(x => x.EffectiveDateTime <= endOfSelectedMonth).ToList();
            if(changeEvents.Count>0)
            {
                changeEvents = changeEvents.OrderBy(change => change.EffectiveDateTime).ToList();

                foreach (var changeEvent in changeEvents)
                {
                    decimal individualChangeAmount = GetIndividualChangeEventAmountDecimal(summingAmount, changeEvent);
                    retVal += individualChangeAmount;
                    summingAmount += individualChangeAmount;
                }
            }
            return retVal;
        }


        //calc change amount lump or percentage
        private static decimal GetIndividualChangeEventAmountDecimal(decimal sumingAmount, ChangeEvent change)
        {
            decimal delta = Decimal.Zero;
            if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.LumpSum))
            {
                delta = change.Amount;
            }
            if (change.ChangeTypeEnum.Equals(ChangeTypeEnum.Percentage))
            {
                delta = sumingAmount * (change.Amount);
            }

            return delta;
        }


        public static int GetFirstTransactionYear()
        {
            int retVal = DateTime.Now.Year;
            PrimaryContext db = new PrimaryContext();
            if (db.Transactions.Any())
            {
                retVal = db.Transactions.Min(x => x.TransactionDate).Year;
            }
            return retVal;
        }

        public static int YearsToDisplay()
        {
            PrimaryContext db = new PrimaryContext();
            int futureYearsDisplay = 2;
            if (db.SystemSettings.Any(x => x.Setting == Enums.SysSetting.NumYearsToDisplay))
            {
                futureYearsDisplay = Convert.ToInt16(db.SystemSettings.FirstOrDefault(x => x.Setting == Enums.SysSetting.NumYearsToDisplay).SettingValue);
            }
            return System.DateTime.Now.Year - GetFirstTransactionYear() + futureYearsDisplay;
        }

        //Run once after upgrading to object based recurrence
        public static void SetAllNullRecurrenceToValue()
        {
            PrimaryContext db = new PrimaryContext();
            foreach (var allocation in db.Allocations.ToList())
            {
                if (allocation.Recurrence == null)
                {
                    allocation.Recurrence = new Recurrence()
                    {
                        RecuranceStartDate = System.DateTime.Today,
                        RecurrenceFrequencyEnum = Models.Enums.RecurrenceEnum.Monthly
                    };
                    db.SaveChanges();
                }
            }
        }

        
    }
}