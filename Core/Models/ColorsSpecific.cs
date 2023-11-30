using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ColorsSpecific
    {
        public int Id { get; set; }
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }

        public virtual Color? Color { get; set; }
        public virtual Product? Product { get; set; }
    }
}
