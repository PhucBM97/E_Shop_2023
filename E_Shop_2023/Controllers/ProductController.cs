using Core.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _prodSrv;
        public ProductController(IProductService sanPhamService)
        {
            _prodSrv = sanPhamService;
        }

        [HttpGet("GetProductList/{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetProductList(int currentPage, int pageSize)
        {
            try
            {
                var result = new ProductListDTO();

                var curren_All_Product = await _prodSrv.GetAllProducts();

                var products = await _prodSrv.GetProductWithPagination(currentPage, pageSize);

                if (!products.Any())
                    return NotFound();

                if (pageSize <= 0)
                    pageSize = 5;
                float numberpage = (float)curren_All_Product.Count() / pageSize;
                int pageCount = (int)Math.Ceiling(numberpage);

                int crrPage = currentPage;
                if (crrPage > pageCount) crrPage = pageCount;

                var model = new List<ProductDetailDTO>();
                foreach (var product in products)
                {
                    model.Add(new ProductDetailDTO
                    {
                        ProductID = product.ProductId,
                        ProductBrand = product.Brand?.BrandName,
                        ProductImage = product.ImageUrl,
                        ProductName = product.ProductName,
                        ProductPrice = product.Price,
                        ProductQuantity = 1
                    });
                }

                result.Products = model;
                result.CurrentPage = crrPage;
                result.PageCount = pageCount;

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductByBrand/{brandId}/{currentPage}/{pageSize}")]
        public async Task<IActionResult> GetProductByBrand(int brandId, int currentPage, int pageSize)
        {
            var result = new ProductListDTO();

            var allProducts = await _prodSrv.GetAllProducts();

            var curren_All_Product = allProducts.Where(p => p.BrandId == brandId);

            var products = await _prodSrv.GetProductByBrand(brandId, currentPage, pageSize);


            //if(!products.Any())
            //    return NotFound();

            if (pageSize <= 0)
                pageSize = 5;
            float numberpage = (float)curren_All_Product.Count() / pageSize;
            int pageCount = (int)Math.Ceiling(numberpage);

            int crrPage = currentPage;
            if (crrPage > pageCount) crrPage = pageCount;

            var model = new List<ProductDetailDTO>();
            foreach (var product in products)
            {
                model.Add(new ProductDetailDTO
                {
                    ProductID = product.ProductId,
                    ProductBrand = product.Brand?.BrandName,
                    ProductImage = product.ImageUrl,
                    ProductName = product.ProductName,
                    ProductPrice = product.Price,
                    ProductQuantity = 1
                });
            }

            result.Products = model;
            result.CurrentPage = crrPage;
            result.PageCount = pageCount;

            return Ok(result);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO entity)
        {
            try
            {
            if (entity is null)
                return BadRequest(new
                {
                    Message = "Entity is null"
                });

            var result = await _prodSrv.CreateProduct(entity);
            if (!result)
            {
                return BadRequest(
                new
                {
                    Message = "Fail"
                });
            }
                return Ok(new
                {
                    Message = "Product Added"
                });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO entity)
        {
            if (entity.ProductId <= 0)
            {
                return BadRequest(new
                {
                    Message = "entity is null"
                }); ;
            }
            var result = await _prodSrv.UpdateProduct(entity);

            if(!result)
            {
                return BadRequest(new
                {
                    Message = "Update Failed !"
                });
            }

            return Ok(new
            {
                Message = "Update successful"
            });
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _prodSrv.DeleteProduct(id);
            if(!result)
                return BadRequest(new
                {
                    Message = "product deletion failed!"
                });

            return Ok(new
            {
                Message = "Product is deleted!"
            });
            // size specific, color specific, images, order detail, inventories
        }

        [HttpGet("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _prodSrv.GetProductById(productId);
            if (product is null)
                return NotFound();
            return Ok(product);
        }


    }
}
