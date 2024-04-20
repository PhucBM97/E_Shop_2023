using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderDetailService
    {
        Task<IEnumerable<OrderDetail>> GetAllOrderDetails();
        Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int orderId);

        Task<bool> AddOrderDetail(OrderDetail orderDetail);

        
    }
}
