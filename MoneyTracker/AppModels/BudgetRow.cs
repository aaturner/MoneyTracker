using MoneyTracker.Models.Allocations;
using MoneyTracker.Utilities;


namespace MoneyTracker.AppModels
{
    public class BudgetRow 
    {

        public BudgetRow()
        {
        }

        public BudgetRow(string column1, string column2, Enums.TableRowType rowType)
        {
            Column1 = column1;
            Column2 = column2;
            RowType = rowType;
        }
        public BudgetRow(string column1, string column2, string column3, string column4, decimal moneyCol1, decimal moneyCol2, 
            Enums.TableRowType rowType, int allocationId)
        {
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            Colomn4 = column4;
            MoneyCol1 = moneyCol1;
            MoneyCol2 = moneyCol2;
            RowType = rowType;
            AllocationId = allocationId;
        }
        //TODO: Chain Constructors


        public BudgetRow(string column1, string column2, string column3, string column4, decimal moneyCol1, decimal moneyCol2,
        decimal moneyCol3, Enums.TableRowType rowType, int allocationId)
        {
            Column1 = column1;
            Column2 = column2;
            Column3 = column3;
            Colomn4 = column4;
            MoneyCol1 = moneyCol1;
            MoneyCol2 = moneyCol2;
            MoneyCol3 = moneyCol3;
            RowType = rowType;
            AllocationId = allocationId;
        }

        public int AllocationId { get; set; }
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
        public string Colomn4 { get; set; }
        public decimal? MoneyCol1 { get; set; }
        public decimal? MoneyCol2 { get; set; }   
        public decimal? MoneyCol3 { get; set; }  //Residual
        public ForecastRow ForecastRow { get; set; }
        public Enums.TableRowType RowType { get; set; }
       

    }
}