using DomainModel.Products;

namespace FurnitureOnlineStore.Models.Products
{
    public class OrderViewModel : Order
    {
        public int SubTotal => Product.Price * Quantity;
    }
}