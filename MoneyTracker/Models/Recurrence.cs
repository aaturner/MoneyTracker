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

        [Required]
        [DisplayName("Recurrence")]
        public Enums.RecurrenceEnum RecurrenceFrequencyEnum { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Effective on")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RecuranceStartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Recur End")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? RecuranceEndDate { get; set; }

        [DisplayName("Day of Month/Week")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public int? RecuranceDayNumber { get; set; }


    }
}