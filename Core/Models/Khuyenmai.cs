using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Khuyenmai
    {
        public Khuyenmai()
        {
            Sanphams = new HashSet<Sanpham>();
        }

        public int Id { get; set; }
        public int LoaiHangId { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public double PhanTramGiam { get; set; }
        public string? QuaTangKem { get; set; }
        public string? VoucherTangKem { get; set; }

        public virtual ICollection<Sanpham> Sanphams { get; set; }
    }
}
