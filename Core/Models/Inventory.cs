using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Product? Product { get; set; }
    }
}
