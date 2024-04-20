using Core.Models;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();

        Task<int> AddOrder(Order order);

        Task<bool> UpdateStatusCode(OrderStatusCodeDTO statusCode);
    }
}
