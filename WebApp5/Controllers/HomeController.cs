using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApp5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService ps;
        private readonly ShoppingCartService shoppingCartService;

        public HomeController(ProductService ps, ShoppingCartService shoppingCartService)
        {
            this.ps = ps;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await ps.GetAll());
        }

        public async Task<IActionResult> Detail(int productId)
        {
            var shoppingCart = await shoppingCartService.Detail(productId);

            if (shoppingCart == null)
            {
                TempData["message"] = "???????????";
                return RedirectToAction(nameof(Index));
            }

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(ShoppingCart shoppingCart)
        {

            shoppingCart.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); //???????????

            await shoppingCartService.Add(shoppingCart);

            return RedirectToAction(nameof(Index));
        }



    }
}
