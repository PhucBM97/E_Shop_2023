using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public int? PaymentId { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderStatusCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Payment? Payment { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
