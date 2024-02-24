using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Inventory
    {
        public int InventoryProsId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Product InventoryPros { get; set; } = null!;
    }
}
