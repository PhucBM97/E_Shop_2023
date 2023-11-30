using System;
using System.Collections.Generic;

namespace Core.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
