using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Products;
using Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Create(Product entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> GetById(int id)
        {
            var entity = await _dbContext.Products.Include(_=>_.Photos).FirstOrDefaultAsync(_ => _.Id == id);
            return entity;
        }

        public async Task<Product> Update(Product entity)
        {
            _dbContext.Products.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _dbContext.Products.FirstOrDefaultAsync(_ => _.Id == id);
            if (entity == null) return false;
            _dbContext.Products.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _dbContext.Products.Include(_=>_.Photos).ToListAsync();
        }

        public async Task<List<Product>> GetRelated(int relatedProductsCount, int currentProductId)
        {
            var currentProduct = await GetById(currentProductId);
            var productsByCurrentCategory = await _dbContext.Products.Include(_ => _.Photos).Where(_ => _.ProductCategoryId == currentProduct.ProductCategoryId).Take(relatedProductsCount).ToListAsync();
            return productsByCurrentCategory.Except(new List<Product>{currentProduct}).ToList();
        }

        public async Task<List<Product>> GetByCategory(int? productCategoryId)
        {
            return await _dbContext.Products.Include(_ => _.Photos).Where(_ => _.ProductCategoryId == productCategoryId).ToListAsync();
        }

        public async Task<List<Product>> GetLast(int lastProductsCount)
        {
            return await _dbContext.Products.Include(_ => _.Photos).OrderBy(_=>_.Created).Take(lastProductsCount).ToListAsync();
        }
    }
}