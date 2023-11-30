using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class SizesSpecific
    {
        public int Id { get; set; }
        public int? SizeId { get; set; }
        public int? ProductId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Size? Size { get; set; }
    }
}
