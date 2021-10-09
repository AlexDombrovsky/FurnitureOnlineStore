using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Products;

namespace Interfaces.Products
{
    public interface IProductService
    {
        Task<Product> Create(Product entity);
        Task<Product> GetById(int id);
        Task<Product> Update(Product entity);
        Task<bool> Delete(int id);
        Task<List<Product>> GetAll();
        Task<List<Product>> GetRelated(int relatedProductsCount, int currentProductId);
        Task<List<Product>> GetByCategory(int? productCategoryId);
        Task<List<Product>> GetLast(int lastProductsCount);
    }
}