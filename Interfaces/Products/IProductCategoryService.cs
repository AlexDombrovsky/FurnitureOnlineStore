using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Products;

namespace Interfaces.Products
{
    public interface IProductCategoryService
    {
        Task<ProductCategory> Create(ProductCategory entity);
        Task<List<ProductCategory>> GetAll();
    }
}