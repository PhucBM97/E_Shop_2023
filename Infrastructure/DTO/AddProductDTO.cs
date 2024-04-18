using Microsoft.AspNetCore.Http;

namespace Infrastructure.DTO
{
    public class AddProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductDescription { get; set; }

        public int ProductCategory {  get; set; }
        public int ProductBrand {  get; set; }
        public IFormFileCollection Images { get; set; }

    }
}
