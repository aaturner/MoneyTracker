using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.AppModels
{
    public class ForecastCenter
    {
        public ForecastCenter()
        {
            ForecastRows = new List<ForecastRow>();
        }

        public List<ForecastRow> ForecastRows { get; set; }

    }
    
}