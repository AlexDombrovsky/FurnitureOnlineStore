using AutoMapper;
using DomainModel.Products;
using FurnitureOnlineStore.Models.Products;

namespace FurnitureOnlineStore.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductViewModel>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Order, OrderViewModel>();
        }
    }
}