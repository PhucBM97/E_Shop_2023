using Core.Interfaces;
using Core.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        public IUnitOfWork _unitOfWork;
        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddOrder(Order order)
        {
            if (order is not null)
            {
                await _unitOfWork.Orders.Add(order);

                var result = _unitOfWork.Save();
                // sau khi gọi savechanges sẽ lấy ra được ID tự sinh ra
                
                var id = order.OrderId;
                return id;
            }
            return 0;
        }

        public Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = _unitOfWork.Orders.GetAll();
            return orders;
        }
    }
}
