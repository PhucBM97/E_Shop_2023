using Core.Interfaces;
using Core.Models;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly E_ShopContext _dbContext;
        public IProductRepository Products { get; }
        public IBrandRepository Brands { get; }
        public IImageRepository Images { get; }

        public ICustomerRepository Customers { get; }

        public IOrderRepository Orders {get; }

        public IOrderDetailRepository OrderDetails { get; }

        public UnitOfWork(
            E_ShopContext dbContext,
            IProductRepository sanphamRepo,
            IBrandRepository brands,
            IImageRepository images,
            ICustomerRepository customers,
            IOrderRepository orders,
            IOrderDetailRepository orderDetails)
        {
            _dbContext = dbContext;
            Products = sanphamRepo;
            Brands = brands;
            Images = images;
            Customers = customers;
            Orders = orders;
            OrderDetails = orderDetails;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }
    }
}
