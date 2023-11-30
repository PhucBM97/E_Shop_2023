using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Size
    {
        public Size()
        {
            SizesSpecifics = new HashSet<SizesSpecific>();
        }

        public int SizeId { get; set; }
        public string? Number { get; set; }
        public string? Details { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<SizesSpecific> SizesSpecifics { get; set; }
    }
}
