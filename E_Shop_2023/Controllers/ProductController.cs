using Core.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var product = await _prodSrv.GetAllProducts();
                if (product is null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductByBrand")]
        public async Task<IActionResult> GetProductByBrand(int brandId)
        {
            var products = await _prodSrv.GetProductByBrand(brandId);
            if(!products.Any())
                return NotFound();
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct(ProductDTO entity)
        {
            try
            {
                if (entity is null)
                    return BadRequest(new
                    {
                        Message = "Entity is null"
                    });
                        var model = new Product
                        {
                            ProductName = entity.ProductName,
                            Price = entity.Price,
                            Description = entity.Description,
                            CreatedDate = DateTime.Now,
                            PromotionId = entity.PromotionId,
                            CategoryId = entity.CategoryId,
                            BrandId = entity.BrandId,
                            Stock = entity.Stock,
                            ImageUrl = entity.ImageUrl
                        };
                        var result = await _prodSrv.CreateProduct(model);
                        if (!result)
                        {
                            return BadRequest(new
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

        [HttpGet("GetProductById/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = _prodSrv.GetProductById(productId);
            if (product is null)
                return NotFound();
            return Ok(product.Result);
        }


    }
}
