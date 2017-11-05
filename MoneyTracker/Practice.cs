using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using MoneyTracker.Models;
using MoneyTracker.Models.Allocations;

namespace MoneyTracker
{
    public class Practice
    {
        private int number;
        private string name;

        public string Name
        {
            get { return String.Format("My name is: {0)", name); }
            set { name = value; }
        }

        public int Number
        {
            get { return number; }
            set { number = value + 2; }
        }

        public static void TestMethod()
        {
            var test = new Person()
            {
                FullName = "New Person", Id = 23,
                Incomes = new Collection<Income>()
                {
                    new Income() {AccountId = 1, Amount = 20, ApplicableMonth = DateTime.Today.AddMonths(1)},
                    new Income(){AccountId = 2, Amount = 25,ApplicableMonth = DateTime.Today}
                }
            };
        }

        //public static ICollection<Income> IncomeFilter(this ICollection<Income> Incomes, decimal threshold)
        //{
        //    return Incomes.Where(x => x.Amount > threshold).ToList();
        //}
    }
}