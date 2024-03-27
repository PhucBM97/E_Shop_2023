using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public readonly IImageService _imageSrv;
        public ImageController(IImageService imageService)
        {
            _imageSrv = imageService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var model = await _imageSrv.GetAllImages();
            if (!model.Any())
                return NotFound();
            return Ok(model);
        }
    }
}
