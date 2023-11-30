using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(Product productDetails);

        Task<IEnumerable<Product>> GetAllProducts();

        Task<Product> GetProductById(int productId);

        Task<bool> UpdateProduct(Product productDetails);

        Task<bool> DeleteProduct(int productId);
    }
}
