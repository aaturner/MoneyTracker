using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models
{
    public class Recurrence
    {
        public int Id { get; set; }
        [DisplayName("Recurrence")]
        public Enums.RecurrenceEnum RecurrenceFrequencyEnum { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Effective on")]
        public DateTime RecuranceStartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Recur End")]
        public DateTime? RecuranceEndDate { get; set; }
        [DisplayName("Day of Month")]
        public int? RecuranceDayNumber { get; set; }
    }
}