using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DomainModel.Products;
using Interfaces.Products;
using Microsoft.EntityFrameworkCore;

namespace Services.Products
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Order>> GetOrdersByUserId(string userId)
        {
            return await _dbContext.Orders.Include(_ => _.Product.Photos).Where(_ => _.UserId == userId).ToListAsync();
        }

        public async Task<Order> GetUsersOrderByProductId(string userId, int productId)
        {
            var order = await _dbContext.Orders.Where(_ => _.UserId == userId).FirstOrDefaultAsync(_ => _.ProductId == productId);
            return order ?? new Order();
        }

        public async Task<Order> Update(Order entity)
        {
            _dbContext.Orders.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<int> GetOrdersCountByUser(string userId)
        {
            return await _dbContext.Orders.Where(_ => _.UserId == userId).CountAsync();
        }
    }
}