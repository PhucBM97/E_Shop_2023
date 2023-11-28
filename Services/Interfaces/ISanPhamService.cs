using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ISanPhamService
    {
        Task<bool> CreateProduct(Sanpham productDetails);

        Task<IEnumerable<Sanpham>> GetAllProducts();

        Task<Sanpham> GetProductById(int productId);

        Task<bool> UpdateProduct(Sanpham productDetails);

        Task<bool> DeleteProduct(int productId);
    }
}
