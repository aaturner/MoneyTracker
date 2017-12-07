using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MoneyTracker.Utilities;

namespace MoneyTracker.Models
{
    public class SystemSetting
    {
        public int ID { get; set; }
        public Utilities.Enums.SysSetting Setting { get; set; }
        [DisplayName("Text")]
        public string SettingValue { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Time")]
        public DateTime? SettingDate { get; set; }
        [DisplayName("Day of Week")]
        public DayOfWeek? SettingDay { get; set; }
        [DisplayName("Whole Number")]
        public int? SettingInt { get; set; }
    }
}