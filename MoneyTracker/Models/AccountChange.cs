using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTracker.Models.ChangeEvents
{
    public class AccountChange : ChangeEvent
    {
        public int AccountId { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual Account Account { get; set; }
    }
}