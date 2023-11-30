using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public int? PromotionId { get; set; }
        public int? ProductTypeId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? Screen { get; set; }
        public string? Cpu { get; set; }
        public string? Gpu { get; set; }
        public string? Ram { get; set; }
        public string? Storage { get; set; }
        public string? HardDisks { get; set; }
        public string? Weight { get; set; }
        public string? ImageUrl { get; set; }
        public string? Camera { get; set; }
        public string? Selfie { get; set; }
        public string? Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? OtherProductDetails { get; set; }
    }
}
