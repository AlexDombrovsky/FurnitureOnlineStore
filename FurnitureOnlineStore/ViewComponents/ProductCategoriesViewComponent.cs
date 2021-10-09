using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FurnitureOnlineStore.Models.Products;
using Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureOnlineStore.ViewComponents
{
    public class ProductCategoriesViewComponent : ViewComponent
    {
        private readonly IMapper _mapper;
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoriesViewComponent(IProductCategoryService productCategoryService, IMapper mapper)
        {
            _productCategoryService = productCategoryService;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetCategories();
            return View(model);
        }

        private async Task<List<ProductCategoryViewModel>> GetCategories()
        {
            var categories = await _productCategoryService.GetAll();
            return _mapper.Map<List<ProductCategoryViewModel>>(categories);
        }
    }
}