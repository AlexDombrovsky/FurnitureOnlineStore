using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Products;
using Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace Services.Products
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductCategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ProductCategory> Create(ProductCategory entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<ProductCategory>> GetAll()
        {
            var entities = await _dbContext.ProductCategories.ToListAsync();
            return entities;
        }
    }
}