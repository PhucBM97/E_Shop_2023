using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public int? PromotionId { get; set; }
        public int? CategoryId { get; set; }
        public int? InventoryId { get; set; }
        public int? BrandId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public bool? Stock { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public string? OtherProductDetails { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
