using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ProductDetailDTO
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public decimal? ProductPrice { get; set; }

        public string? ProductBrand {  get; set; }

        public string? ProductImage { get; set; }

        public int? ProductQuantity { get; set; }
    }
}
