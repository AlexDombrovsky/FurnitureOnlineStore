using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel.Products;

namespace Interfaces.Products
{
    public interface IOrderService
    {
        Task<Order> Create(Order entity);
        Task<List<Order>> GetOrdersByUserId(string userId);
        Task<Order> GetUsersOrderByProductId(string userId, int productId);
        Task<Order> Update(Order entity);
        Task<int> GetOrdersCountByUser(string userId);
    }
}