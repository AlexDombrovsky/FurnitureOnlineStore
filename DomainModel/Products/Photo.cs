namespace DomainModel.Products
{
    public class Photo : BaseEntity
    {
        public string Path { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}