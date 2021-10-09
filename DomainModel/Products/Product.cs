using System.Collections.Generic;

namespace DomainModel.Products
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo>();
    }
}