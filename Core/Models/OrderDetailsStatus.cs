using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class OrderDetailsStatus
    {
        public int OrderItemStatusCode { get; set; }
        public string? Description { get; set; }
    }
}
