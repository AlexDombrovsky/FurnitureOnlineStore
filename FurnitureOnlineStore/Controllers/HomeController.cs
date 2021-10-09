using System.Threading.Tasks;
using Interfaces.Products;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private const int LastProductsCount = 3;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetLast(LastProductsCount);
            return View(products);
        }
    }
}