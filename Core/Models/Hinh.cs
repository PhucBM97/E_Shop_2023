using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Hinh
    {
        public int HinhId { get; set; }
        public string Thumbnails { get; set; } = null!;
        public string? Carousel { get; set; }

        public virtual Sanpham HinhNavigation { get; set; } = null!;
    }
}
