using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrdersDTO
    {
        public int? OrderID { get; set; }
        public string? CustomerName { get; set; }

        public string? CustomerPhone { get; set; }

        public string? CustomerAddress { get; set; }
        public int? OrderStatusCode { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? OrderTotal{ get; set; }
    }
}
