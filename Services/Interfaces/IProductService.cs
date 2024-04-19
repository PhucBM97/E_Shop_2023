using Core.Models;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<int> CreateProduct(Product productDetails);

        Task<IEnumerable<Product>> GetProductWithPagination(int currentPage, int pageSize = 10);
        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(int productId);

        int UpdateProduct(Product product);

        Task<bool> DeleteProduct(int productId);

        Task<IEnumerable<Product>> GetProductByBrand(int brandId, int currentPage, int pageSize = 6);

        Task<bool> Delete(int productId);

    }
}
