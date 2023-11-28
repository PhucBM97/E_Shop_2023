using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        public readonly ISanPhamService _sanphamSrv;
        public SanPhamController(ISanPhamService sanPhamService)
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
