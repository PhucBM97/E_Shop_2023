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
                //var product = _unitOfWork.Products.GetById(productDetails.ProductId);
                    await _unitOfWork.Products.Add(productDetails);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.Products.GetById(productId);
                if( product is not null)
                {
                    _unitOfWork.Products.Delete(product);
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
            var products = await _unitOfWork.Products.GetAll();
            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.Products.GetById(productId);
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
                var product = await _unitOfWork.Products.GetById(productDetails.ProductId);
                if (product is not null)
                {
                    //product.ProductName = productDetails.ProductName;
                    //product.Cpu = productDetails.Cpu;
                    //product.Gpu = productDetails.Gpu;
                    //product.HardDisks = productDetails.HardDisks;
                    //product.Camera = productDetails.Camera;
                    //product.Selfie = productDetails.Selfie;
                    //product.Screen = productDetails.Screen;
                    //product.Ram = productDetails.Ram;
                    //product.Description = productDetails.Description;
                    //product.ImageUrl = productDetails.ImageUrl;
                    //product.Price = productDetails.Price;
                    //product.OtherProductDetails = productDetails.OtherProductDetails;
                    //product.ProductTypeId = productDetails.ProductTypeId;
                    //product.PromotionId = productDetails.PromotionId;
                    //product.Weight = productDetails.Weight;


                    _unitOfWork.Products.Update(product);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                }
            }
            return false;
        }
    }
}
