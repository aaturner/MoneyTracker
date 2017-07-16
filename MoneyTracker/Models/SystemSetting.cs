using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models
{
    public class SystemSetting
    {
        public int ID { get; set; }
        public Utilities.Enums.SysSetting Setting { get; set; }
        public string SettingValue { get; set; }
        public DateTime SettingDate { get; set; }
    }
}