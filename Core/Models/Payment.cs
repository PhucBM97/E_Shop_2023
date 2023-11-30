using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Orders = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public string? PaymentName { get; set; }
        public bool? Allowed { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
