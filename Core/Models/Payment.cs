using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public string? PaymentName { get; set; }
        public bool? Allowed { get; set; }
    }
}
