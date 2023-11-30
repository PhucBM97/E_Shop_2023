using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly IProductService _sanphamSrv;
        public ProductController(IProductService sanPhamService)
        {
            _sanphamSrv = sanPhamService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            try
            {
                var product = await _sanphamSrv.GetAllProducts();
                if (product is null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
