using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class User
    {
        public User()
        {
            Donhangs = new HashSet<Donhang>();
            Sanphams = new HashSet<Sanpham>();
        }

        public int Id { get; set; }
        public string Ten { get; set; } = null!;
        public string? TenLot { get; set; }
        public string? SoDienThoai { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime? NgayTao { get; set; }
        public string? ThongTin { get; set; }
        public string? Diachi { get; set; }
        public string? ThanhPho { get; set; }
        public bool? KichHoat { get; set; }

        public virtual ICollection<Donhang> Donhangs { get; set; }
        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
