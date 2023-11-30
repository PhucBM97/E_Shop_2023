using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class ProductType
    {
        public int ProductTypeId { get; set; }
        public string? ProductTypeName { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? Description { get; set; }
    }
}
