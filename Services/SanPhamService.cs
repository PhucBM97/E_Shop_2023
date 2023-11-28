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
    public class SanPhamService : ISanPhamService
    {
        public IUnitOfWork _unitOfWork;
        public SanPhamService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(Sanpham productDetails)
        {
            if (productDetails is not null)
            {
                var product = _unitOfWork.SanPhams.GetById(productDetails.Id);
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

        public async Task<IEnumerable<Sanpham>> GetAllProducts()
        {
            var products = await _unitOfWork.SanPhams.GetAll();
            return products;
        }

        public async Task<Sanpham> GetProductById(int productId)
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

        public async Task<bool> UpdateProduct(Sanpham productDetails)
        {
            if (productDetails is not null)
            {
                var product = await _unitOfWork.SanPhams.GetById(productDetails.Id);
                if (product is not null)
                {
                    product.TenSanPham = productDetails.TenSanPham;
                    product.MaSanPham = productDetails.MaSanPham;
                    product.HinhSanPham = productDetails.HinhSanPham;
                    product.GiaSanPham = productDetails.GiaSanPham;

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
