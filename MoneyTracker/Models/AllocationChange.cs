using MoneyTracker.Models.Allocations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models.ChangeEvents
{
    public class AllocationChange: ChangeEvent
    {
        [Required]
        public int AllocationId { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Allocation Allocation { get; set; }
    
    }
}