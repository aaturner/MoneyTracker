using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MoneyTracker.Models.DataObjects;

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
    }
}