using System.Collections.Generic;

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
        }

        public List<BudgetRow> BudgetRows { get; set; }
        public int SelectedMonth { get; set; }
    }
}