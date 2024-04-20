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
    public class OrderDetailService : IOrderDetailService
    {
        public IUnitOfWork _unitOfWork;
        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddOrderDetail(OrderDetail orderDetail)
        {
            if (orderDetail is not null)
            {
                await _unitOfWork.OrderDetails.Add(orderDetail);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
            }
            return false;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetails()
        {
            var orderDetails = await _unitOfWork.OrderDetails.GetAll();
            return orderDetails;
        }

        public async Task<IEnumerable<OrderDetail>> GetOrderDetailByOrderId(int orderId)
        {
            var orderDetail = await _unitOfWork.OrderDetails.GetDataWithPredicate(p => p.OrderId == orderId);
            return orderDetail;
        }
    }
}
