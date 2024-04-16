using Core.Interfaces;
using Core.Models;
using Infrastructure.DTO;
using Services.Interfaces;

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
                    ImageUrl = product.ImageUrl,
                    IsDeleted = false
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
            if(productId < 0) return false;

            var model = await _unitOfWork.Products.GetById(productId);

            if(model is null) return false;
            model.IsDeleted = true;
            _unitOfWork.Products.Update(model);
            var result = _unitOfWork.Save();
            if(result == 0) return false;
            return true;
           
        }

        public async Task<IEnumerable<Product>> GetProductWithPagination(int currentPage, int pageSize = 5)
        {
            var allProducts = await _unitOfWork.Products.GetAll();

            var products = allProducts.Where(p => p.IsDeleted == false);

            if (pageSize <= 0)
                pageSize = 5;

            float numberpage = (float)products.Count() / pageSize;
            int pageCount = (int)Math.Ceiling(numberpage);

            if (currentPage > pageCount) currentPage = pageCount;

            products = products
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);
            //

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

        public async Task<IEnumerable<Product>> GetProductByBrand(int brandId, int currentPage, int pageSize = 6)
        {
            var products = await _unitOfWork.Products.GetDataWithPredicate(p => p.BrandId == brandId && p.IsDeleted == false);

            if (pageSize <= 0)
                pageSize = 5;

            float numberpage = (float)products.Count() / pageSize;
            int pageCount = (int)Math.Ceiling(numberpage);

            if (currentPage > pageCount) currentPage = pageCount;

            products = products
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);


            var brands = await _unitOfWork.Brands.GetAll();

            foreach (var item in products)
            {
                item.Brand = brands.Where(p => p.BrandId == item.BrandId).FirstOrDefault();
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var allProducts = await _unitOfWork.Products.GetAll();

            var products = allProducts.Where(p => p.IsDeleted == false);

            var brands = await _unitOfWork.Brands.GetAll();

            foreach (var item in products)
            {
                item.Brand = brands.Where(p => p.BrandId == item.BrandId).FirstOrDefault();
            }

            return products;
        }
    }
}
