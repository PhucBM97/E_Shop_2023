using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderDetailsDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImg { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }

        public decimal SubTotal {  get; set; }

    }
}
