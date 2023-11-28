using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Chucnang
    {
        public int SanPhamId { get; set; }
        public int? LoaiHangId { get; set; }
        public string? ThietKe { get; set; }
        public string? BoXuLy { get; set; }
        public string? ManHinh { get; set; }
        public string? Vga { get; set; }
        public string? BoNhoLuuTru { get; set; }
        public string? BaoMat { get; set; }
        public string? GiaoTiepKetNoi { get; set; }
        public string? Camera { get; set; }
        public string? Amthanh { get; set; }
        public string? BaoHanhId { get; set; }
        public string? Pin { get; set; }
        public string? HeDieuHanh { get; set; }

        public virtual Loaihang? LoaiHang { get; set; }
        public virtual Sanpham SanPham { get; set; } = null!;
    }
}
