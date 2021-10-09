using System.Threading.Tasks;
using Interfaces.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureOnlineStore.ViewComponents
{
    public class CartIconViewComponent : ViewComponent
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<IdentityUser> _userManager;

        public CartIconViewComponent(IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await GetOrdersCount();
            return View(result);
        }

        private async Task<int> GetOrdersCount()
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            return await _orderService.GetOrdersCountByUser(userId);
        }
    }
}