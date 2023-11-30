using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int CartId { get; set; }
        public int? CustomerId { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
