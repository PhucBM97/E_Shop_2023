using Core.Models;
using Infrastructure.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace E_Shop_2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _prodSrv;
        private readonly IFileService _fileSrv;
        private readonly IImageService _imageSrv;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IProductService sanPhamService, IFileService fileService, IImageService imageService, IHttpContextAccessor httpContextAccessor)
        {
            _prodSrv = sanPhamService;
            _fileSrv = fileService;
            _imageSrv = imageService;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
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

        [AllowAnonymous]
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
        public async Task<IActionResult> AddProduct([FromForm] AddProductDTO entity) // dùng form thì [fromform]
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var domain = $"{request.Scheme}://{request.Host}";

            if (entity is null)
                return BadRequest(new
                {
                    Message = "entity is null"
                });

            var listImageUrl = new List<string>();
            foreach (var image in entity.Images)
            {
                var imgUrl = $"{domain}{await _fileSrv.UploadFile(image)}";
                listImageUrl.Add(imgUrl);
            }

            int add_Or_Update_Result = 0;

            // add
            if(entity.ProductId <= 0)
            {
                var product = new Product
                {
                    ProductId = entity.ProductId,
                    ProductName = entity.ProductName,
                    Price = entity.ProductPrice,
                    Description = entity.ProductDescription,
                    CreatedDate = DateTime.Now,
                    PromotionId = 1,
                    CategoryId = entity.ProductCategory,
                    BrandId = entity.ProductBrand,
                    ImageUrl = listImageUrl.FirstOrDefault(),
                    Stock = true,
                    IsDeleted = false
                };

                add_Or_Update_Result = await _prodSrv.CreateProduct(product);

                if (add_Or_Update_Result <= 0)
                    return BadRequest(new
                    {
                        Message = "Add new product failed!"
                    });

                foreach (var url in listImageUrl.Skip(1))
                {
                    await _imageSrv.CreateImage(new Image
                    {
                        ProductId = add_Or_Update_Result,
                        Path = url
                    });
                }
            }
            // update
            else
            {
                var existModel = await _prodSrv.GetProductById(entity.ProductId);
                if (existModel is null)
                    return NotFound(new
                    {
                        Message = "Not found product !"
                    });

                    existModel.ProductName = entity.ProductName;
                    existModel.Price = entity.ProductPrice;
                    existModel.Description = entity.ProductDescription;
                    existModel.UpdatedDate = DateTime.Now;
                    existModel.PromotionId = 1;
                    existModel.CategoryId = entity.ProductCategory;
                    existModel.BrandId = entity.ProductBrand;
                    existModel.ImageUrl = listImageUrl.FirstOrDefault();
                    existModel.Stock = true;
                    existModel.IsDeleted = false;

                add_Or_Update_Result = _prodSrv.UpdateProduct(existModel);

                if(add_Or_Update_Result <= 0)
                    return BadRequest(new
                    {
                        Message = "Product update failed !"
                    });

                var exitsImg = await _imageSrv.GetIamgesByProductId(existModel.ProductId);

                // delete -> add
                if (exitsImg.Any())
                {
                    foreach (var img in exitsImg)
                    {
                        _imageSrv.DeleteImage(img);
                    }
                }
                //
                foreach (var url in listImageUrl.Skip(1))
                {
                    await _imageSrv.CreateImage(new Image
                    {
                        ProductId = add_Or_Update_Result,
                        Path = url
                    });
                }

            }

            //if (!imgResult)
            //    return BadRequest();

            return Ok(new
            {
                Message = "Successful"
            });
        }

        //[HttpPut("UpdateProduct")]
        //public async Task<IActionResult> UpdateProduct([FromBody] ProductDTO entity)
        //{
        //    if (entity.ProductId <= 0)
        //    {
        //        return BadRequest(new
        //        {
        //            Message = "entity is null"
        //        }); ;
        //    }
        //    var result = await _prodSrv.UpdateProduct(entity);

        //    if(!result)
        //    {
        //        return BadRequest(new
        //        {
        //            Message = "Update Failed !"
        //        });
        //    }

        //    return Ok(new
        //    {
        //        Message = "Update successful"
        //    });
        //}

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _prodSrv.DeleteProduct(id);
            if(!result)
                return BadRequest(new
                {
                    Message = "product not found!"
                });

            return Ok(new
            {
                Message = "Product is deleted!"
            });
            // size specific, color specific, images, order detail, inventories
        }

        [AllowAnonymous]
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
