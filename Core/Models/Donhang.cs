using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Donhang
    {
        public Donhang()
        {
            Chitietdonhangs = new HashSet<Chitietdonhang>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public short TinhTrangDonHang { get; set; }
        public double GiamGia { get; set; }
        public double PhiShip { get; set; }
        public double TongTien { get; set; }
        public string MaGiamGia { get; set; } = null!;
        public string Ten { get; set; } = null!;
        public string SoDienThoai { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public DateTime NgayTao { get; set; }
        public DateTime NgayCapNhat { get; set; }
        public string NoiDung { get; set; } = null!;

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; }
    }
}
