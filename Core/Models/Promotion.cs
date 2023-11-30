using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Promotion
    {
        public Promotion()
        {
            Products = new HashSet<Product>();
        }

        public int PromotionId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Discount { get; set; }
        public string? Gift { get; set; }
        public string? Voucher { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
