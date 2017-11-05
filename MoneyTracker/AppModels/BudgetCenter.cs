using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoneyTracker.AppModels
{
    public class BudgetCenter
    {
        public BudgetCenter()
        {
            List<BudgetRow> BudgetRows= new List<BudgetRow>();
        }
        public BudgetCenter(List<BudgetRow> rowList )
        {
            BudgetRows = rowList;
            SelectedMonth = System.DateTime.Now.Month;
            SelectedYear = System.DateTime.Now.Year;
        }

        public List<BudgetRow> BudgetRows { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
    //    [DataType(DataType.Date)]
    //    [DisplayFormat(DataFormatString = "{0:yyyy/MM}", ApplyFormatInEditMode = true)]
    //    public DateTime SelectedPeriod { get; set; }
    }
}