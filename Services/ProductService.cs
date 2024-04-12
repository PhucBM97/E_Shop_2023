using Core.Interfaces;
using Core.Models;
using Infrastructure.DTO;
using Services.Interfaces;
using System;
using System.Collections;
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

        public async Task<bool> CreateProduct(ProductDTO product)
        {
            if (product is not null)
            {
                //var product = _unitOfWork.Products.GetById(productDetails.ProductId);
                var model = new Product
                {
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Description = product.Description,
                    CreatedDate = DateTime.Now,
                    PromotionId = product.PromotionId,
                    CategoryId = product.CategoryId,
                    BrandId = product.BrandId,
                    Stock = product.Stock,
                    ImageUrl = product.ImageUrl
                };
            await _unitOfWork.Products.Add(model);

            var result = _unitOfWork.Save();

            if (result <= 0)
                return false;
            }
            return true;
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

            var brands = await _unitOfWork.Brands.GetAll();

            foreach (var item in products)
            {
                item.Brand = brands.Where(p => p.BrandId == item.BrandId).FirstOrDefault();
            }

            return products;
        }

        public async Task<Product> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var product = await _unitOfWork.Products.GetById(productId);

                var images = await _unitOfWork.Images.GetDataWithPredicate(p => p.ProductId == product.ProductId);

                product.Images = (ICollection<Image>)images;
                
                if (product is not null)
                {
                    return product;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProduct(ProductDTO product)
        {
            if(product is null)
                return false;
            
            var model = await _unitOfWork.Products.GetById(product.ProductId);

            if (model is null)
                return false;

            model.ProductName = product.ProductName;
            model.Price = product.Price;
            model.Description = product.Description;
            model.UpdatedDate = DateTime.Now;
            model.PromotionId = product.PromotionId;
            model.CategoryId = product.CategoryId;
            model.BrandId = product.BrandId;
            model.Stock = product.Stock;
            model.ImageUrl = product.ImageUrl;

            _unitOfWork.Products.Update(model);
            var result = _unitOfWork.Save();
            if (result <= 0)
                return false;

            return true;
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(int brandId)
        {
            var products = await _unitOfWork.Products.GetDataWithPredicate(p => p.BrandId == brandId);

            var brands = await _unitOfWork.Brands.GetAll();

            foreach (var item in products)
            {
                item.Brand = brands.Where(p => p.BrandId == item.BrandId).FirstOrDefault();
            }
            return products;
        }
    }
}
