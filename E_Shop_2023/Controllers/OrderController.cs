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
        private readonly ICustomerService _customerSrv;
        private readonly IOrderService _orderSrv;
        private readonly IOrderDetailService _orderDetailSrv;
        private readonly IProductService _productSrv;

        
        public OrderController(
            ICustomerService customerService,
            IOrderService orderService,
            IOrderDetailService orderDetailService,
            IProductService productService)
        {
            _customerSrv = customerService;
            _orderSrv = orderService;
            _orderDetailSrv = orderDetailService;
            _productSrv = productService;
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
            //phone + email
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
                PaymentId = ((int)Constants.Payment.Offline), // 1 = tt trực tiếp
                CustomerId = checkCus.CustomerId, // lấy id của khách hàng ở trên ( theo email )
                OrderStatusCode = ((int)Constants.OrderStatus.Pending), // 
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
                    Quantity = info.ProductQuantity,
                    Price = info.ProductPrice,
                    SubTotal = info.ProductSubTotal,
                });
            }
            return Ok(new
            {
                Message = "Thành công !"
            });

        }

        [HttpGet("getorders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderSrv.GetAllOrders();

            var customers = await _customerSrv.GetAllCustomers();

            foreach (var order in orders)
            {
                order.Customer = customers.FirstOrDefault(p => p.CustomerId == order.CustomerId);
            }

            var orderModel = new List<OrdersDTO>();

            foreach (var order in orders)
            {
                orderModel.Add(new OrdersDTO
                {
                    OrderID = order.OrderId,
                    CustomerName = order.Customer?.FullName,
                    CustomerPhone = order.Customer?.Phone,
                    CustomerAddress = order.Customer?.Address,
                    OrderStatusCode = order.OrderStatusCode,
                    OrderDate = order.OrderDate,
                    OrderTotal = order.Total
                });
            }
            return Ok(orderModel);

        }
        [HttpGet("getorderdetail/{orderId}")]
        public async Task<IActionResult> GetOrderDetail(int orderId)
        {
            var orderDetails = await _orderDetailSrv.GetOrderDetailByOrderId(orderId);

            foreach (var item in orderDetails)
            {
                item.Product = await _productSrv.GetProductById(item.ProductId ?? 0);
            }

            var model = new List<OrderDetailsDTO>();

            foreach (var info in orderDetails)
            {
                model.Add(new OrderDetailsDTO
                {
                    ProductId = info.Product.ProductId,
                    ProductName = info.Product.ProductName,
                    ProductImg = info.Product.ImageUrl,
                    ProductPrice = info.Price ?? 0,
                    ProductQuantity = info.Quantity ?? 0,
                    SubTotal = info.SubTotal ?? 0,
                });
            }


            return Ok(model);
        }

    }
}
