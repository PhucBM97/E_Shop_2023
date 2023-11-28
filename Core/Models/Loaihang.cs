using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Loaihang
    {
        public int Id { get; set; }
        public string TenLoaiHang { get; set; } = null!;
        public string NoiDung { get; set; } = null!;
    }
}
