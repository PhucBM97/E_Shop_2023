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
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(Product productDetails)
        {
            if (productDetails is not null)
            {
                var product = _unitOfWork.SanPhams.GetById(productDetails.ProductId);
                if( product is null)
                {
                    await _unitOfWork.SanPhams.Add(productDetails);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.SanPhams.GetById(productId);
                if( product is not null)
                {
                    _unitOfWork.SanPhams.Delete(product);
                    var result = _unitOfWork.Save();
                    if (result > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _unitOfWork.SanPhams.GetAll();
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.SanPhams.GetById(productId);
                if (product is not null)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProduct(Product productDetails)
        {
            if (productDetails is not null)
            {
                var product = await _unitOfWork.SanPhams.GetById(productDetails.ProductId);
                if (product is not null)
                {
                    //product.TenSanPham = productDetails.TenSanPham;
                    //product.MaSanPham = productDetails.MaSanPham;
                    //product.HinhSanPham = productDetails.HinhSanPham;
                    //product.GiaSanPham = productDetails.GiaSanPham;

                    // db chưa đúng

                    _unitOfWork.SanPhams.Update(product);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                }
            }
            return false;
        }
    }
}
