using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Color
    {
        public Color()
        {
            ColorsSpecifics = new HashSet<ColorsSpecific>();
        }

        public int ColorId { get; set; }
        public string? ColorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<ColorsSpecific> ColorsSpecifics { get; set; }
    }
}
