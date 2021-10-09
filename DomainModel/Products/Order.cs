namespace DomainModel.Products
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}