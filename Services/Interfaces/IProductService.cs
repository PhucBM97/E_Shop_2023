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
        Task<bool> CreateProduct(ProductDTO productDetails);

        Task<IEnumerable<Product>> GetAllProducts ();

        Task<Product> GetProductById(int productId);

        Task<bool> UpdateProduct(ProductDTO productDetails);

        Task<bool> DeleteProduct(int productId);

        Task<IEnumerable<Product>> GetProductByBrand(int brandId);


    }
}
