using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusCode { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? SubTotal { get; set; }
        public string? OtherOrderItemDetails { get; set; }
    }
}
