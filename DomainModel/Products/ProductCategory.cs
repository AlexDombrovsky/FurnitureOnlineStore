using System.Collections.Generic;

namespace DomainModel.Products
{
    public class ProductCategory : BaseEntity
    {
        public string Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}