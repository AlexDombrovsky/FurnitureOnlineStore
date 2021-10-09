using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FurnitureOnlineStore.Models.Products;
using Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureOnlineStore.ViewComponents
{
    public class RelatedProductsViewComponent : ViewComponent
    {
        private const int RelatedProductsCount = 16;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public RelatedProductsViewComponent(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int currentProductId)
        {
            var model = await GetRelatedProducts(currentProductId);
            return View(model);
        }

        public async Task<List<ProductViewModel>> GetRelatedProducts(int productCategoryId)
        {
            var products = await _productService.GetRelated(RelatedProductsCount, productCategoryId);
            var model = _mapper.Map<List<ProductViewModel>>(products);
            return model;
        }
    }
}