using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderDetailDTO
    {
        public int OrderDetailId { get; set; }
        public int? ProductId { get; set; }
        public int? OrderId { get; set; }
        public int? StatusCode { get; set; }
        public int? ProductQuantity { get; set; }
        public decimal? ProductPrice { get; set; }
        public decimal? ProductSubTotal { get; set; }
        public string? OtherOrderItemDetails { get; set; }
    }
}
