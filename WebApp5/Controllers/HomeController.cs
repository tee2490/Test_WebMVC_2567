using Microsoft.AspNetCore.Mvc;

namespace WebApp5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService ps;
        private readonly ShoppingCartService shoppingCartService;

        public HomeController(ProductService ps,ShoppingCartService shoppingCartService )
        {
            this.ps = ps;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await ps.GetAll());
        }

        public async Task<IActionResult> Detail(int id)
        {
            var shoppingCart = await shoppingCartService.Detail(id);

            if (shoppingCart == null)
            {
                TempData["message"] = "???????????";
                return RedirectToAction(nameof(Index));
            }

            return View(shoppingCart);
        }


    }
}
