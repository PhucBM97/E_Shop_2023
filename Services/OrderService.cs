using Core.Interfaces;
using Core.Models;
using Infrastructure.DTO;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return orders;
        }

        public async Task<bool> UpdateStatusCode(OrderStatusCodeDTO statusCode)
        {
            var order = await _unitOfWork.Orders.GetById(statusCode.OrderId);
            
            if (order is null) return false;

            order.UpdatedDate = DateTime.Now;
            order.OrderStatusCode = statusCode.Code;
            _unitOfWork.Orders.Update(order);
            var result = _unitOfWork.Save();
            if(result <= 0 ) return false;

            return true;
        }

    }
}
