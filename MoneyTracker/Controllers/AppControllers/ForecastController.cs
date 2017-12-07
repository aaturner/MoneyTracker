using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MoneyTracker.AppModels;
using MoneyTracker.DAL;
using MoneyTracker.Models;
using MoneyTracker.Utilities;

namespace MoneyTracker.Controllers.AppControllers
{
    [Authorize]
    public class ForecastController : Controller
    {
        public ActionResult Index(int selectedMonth = 0)
        {
            ForecastCenter forecastCenter = new ForecastCenter();
            forecastCenter.ForecastRows = ForecastRowUtils.BuildForecastRows();
            
            //TODO figure out month int (no year) versus current date issue, use recurance day when applicable
            return View(forecastCenter);
        }



        public IEnumerable<SelectListItem> SelectedMonth
        {
            get
            {
                return DateTimeFormatInfo
                    .InvariantInfo
                    .MonthNames
                    .Select((monthName, index) => new SelectListItem()
                    {
                        Value = (index + 1).ToString(),
                        Text = monthName
                    });
            }
        }
    }
}
//TODO: Add button to edit a budget item
//TODO: Add button to add budget item to a category
//TODO: Add button to add a catagory
//TODO: How to do drag and drop a line from one catagory to another?