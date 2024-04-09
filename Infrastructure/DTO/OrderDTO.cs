using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DTO
{
    public class OrderDTO
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderDelivery {  get; set; }
        public decimal OrderTotal { get; set; }
        
        public List<OrderDetailDTO> orderDetail { get; set; }

    }
}
