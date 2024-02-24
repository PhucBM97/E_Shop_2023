using Core.Models;
using Infrastructure.DTO;
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

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductDTO entity)
        {
            try
            {
                if (entity is not null)
                {
                    //var product = await _prodSrv.GetProductById(entity.ProductId);
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
                        if (result)
                        {
                            return Ok(entity);
                        }
                        else
                        {
                            return BadRequest(result);
                        }

                }
                return BadRequest("Sản phẩm đã tồn tại");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
