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

        [HttpGet("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var product = await _prodSrv.GetAllProducts();
                if (product is null)
                    return NotFound();
                var model = new List<ProductDetailDTO>();
                foreach (var info in product)
                {
                    model.Add(new ProductDetailDTO
                    {
                        ProductID = info.ProductId,
                        ProductBrand = info.Brand?.BrandName,
                        ProductImage = info.ImageUrl,
                        ProductName = info.ProductName,
                        ProductPrice = info.Price,
                        ProductQuantity = 1
                    });
                }

                return Ok(model);
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
            return Ok();
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
