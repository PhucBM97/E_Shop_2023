using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        public readonly IBrandService _brandSrv;
        public BrandController(IBrandService brandService)
        {
            _brandSrv = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var brands = await _brandSrv.GetAllBrands();
                if (brands is null)
                    return NotFound();

                return Ok(brands);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
