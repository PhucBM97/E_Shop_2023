using Core.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly ICustomerService _customerSrv;
        public readonly IOrderService _orderSrv;
        public readonly IOrderDetailService _orderDetailSrv;
        
        public OrderController(
            ICustomerService customerService,
            IOrderService orderService,
            IOrderDetailService orderDetailService)
        {
            _customerSrv = customerService;
            _orderSrv = orderService;
            _orderDetailSrv = orderDetailService;
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            var model = await _customerSrv.GetAllCustomers();
            return Ok(model);
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDTO entity)
        {
            var cus = await _customerSrv.GetCustomerByEmail(entity.CustomerEmail);
            if(cus is null)
            {
                var customer = new Customer
                {
                    Address = entity.CustomerAddress,
                    FullName = entity.CustomerName,
                    Phone = entity.CustomerPhone,
                    Email = entity.CustomerEmail,
                    CreatedDate = DateTime.Now,
                };
                await _customerSrv.AddCustomer(customer);
            }
            //
            var checkCus = await _customerSrv.GetCustomerByEmail(entity.CustomerEmail);

            var order = new Order
            {
                PaymentId = 1, // 1 = tt trực tiếp
                CustomerId = checkCus.CustomerId, // lấy id của khách hàng ở trên ( theo email )
                OrderStatusCode = 1, // 
                OrderDate = DateTime.Now,
                Delivery = entity.OrderDelivery, // home hoặc store
                Total = entity.OrderTotal, // tổng giá tiền 
                CreatedDate = DateTime.Now
            };

            var orderId = await _orderSrv.AddOrder(order);


            foreach (var info in entity.orderDetail)
            {
                await _orderDetailSrv.AddOrderDetail(new OrderDetail
                {
                    ProductId = info.ProductId,
                    OrderId = orderId,
                    Quantity = info.Quantity,
                    Price = info.Price,
                    SubTotal = info.SubTotal,
                });
            }
            return Ok(new
            {
                Message = "Thành công !"
            });

        }

        [HttpPost("addor")]
        public async Task<IActionResult> addor(OrderDTO entity)
        {
            var result = await _orderSrv.AddOrder(new Order
            {
                PaymentId = 1,
                CustomerId = 1,
                Delivery = "home",
                Total = 1000000,
            });
            if (result == 0)
                return BadRequest();
            return Ok(result);
        }
    }
}
