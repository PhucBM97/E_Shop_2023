using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class ProductListDTO
    {
        public List<ProductDetailDTO> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
