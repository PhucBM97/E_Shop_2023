using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly E_ShopContext _dbContext;
        public IProductRepository Products { get; }
        public IBrandRepository Brands { get; }
        public IImageRepository Images { get; }

        public UnitOfWork(
            E_ShopContext dbContext,
            IProductRepository sanphamRepo,
            IBrandRepository brands,
            IImageRepository images)
        {
            _dbContext = dbContext;
            Products = sanphamRepo;
            Brands = brands;
            Images = images;

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
