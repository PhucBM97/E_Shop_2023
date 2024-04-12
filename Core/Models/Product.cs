using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            ColorsSpecifics = new HashSet<ColorsSpecific>();
            Images = new HashSet<Image>();
            OrderDetails = new HashSet<OrderDetail>();
            SizesSpecifics = new HashSet<SizesSpecific>();
        }

        public int ProductId { get; set; }
        public int? PromotionId { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public bool? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? OtherProductDetails { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Brand? Brand { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Promotion? Promotion { get; set; }
        public virtual Inventory? Inventory { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<ColorsSpecific> ColorsSpecifics { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<SizesSpecific> SizesSpecifics { get; set; }
    }
}
