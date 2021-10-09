using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DomainModel.Products;
using FurnitureOnlineStore.Models.Products;
using Interfaces.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureOnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager, IMapper mapper, SignInManager<IdentityUser> signInManager)
        {
            _orderService = orderService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if(user == null)
            {
               return RedirectToAction("Login", "Account");
            }
            var orders = await _orderService.GetOrdersByUserId(user.Id);
            return View(_mapper.Map<List<OrderViewModel>>(orders));
        }

        public async Task<IActionResult> AddToCart(ProductViewModel model, int quantity)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var user = await _userManager.GetUserAsync(User);
                var order = await _orderService.GetUsersOrderByProductId(user.Id, model.Id);
                if (order.Id == 0)
                {
                    await _orderService.Create(new Order
                    {
                        UserId = user.Id,
                        ProductId = model.Id,
                        Quantity = quantity
                    });
                }
                else
                {
                    order.Quantity += quantity;
                    await _orderService.Update(order);
                }
            }

            return RedirectToAction("Details", "Product", new {id = model.Id});
        }
    }
}