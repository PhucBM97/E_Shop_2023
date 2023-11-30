using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public int? OrderStatusCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Total { get; set; }
    }
}
