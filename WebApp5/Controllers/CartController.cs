using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp5.Services;

namespace WebApp5.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService cartService;

        [BindProperty] //ให้ผูกออบเจคโดยอัตโนมัติเทียบเท่าการส่งพารามิเตอร์
        public ShoppingCartDto shoppingCartDto { get; set; }


        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var shoppingCartDto = await cartService.GetAll(userId);

            return View(shoppingCartDto);
        }

        public async Task<IActionResult> Plus(int cartId)
        {
          await cartService.Plus(cartId);
          return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Minus(int cartId)
        {
            await cartService.Minus(cartId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int cartId)
        {
           await cartService.Remove(cartId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Summary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            shoppingCartDto = await cartService.Summary(userId);

            return View(shoppingCartDto);
        }


        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SummaryPOST(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var success = await cartService.SummaryPost(shoppingCartDto,userId,file);

            string message = "ชำระเงินไม่สำเร็จ";
            if (success) message = "ชำระเงินสำเร็จ";
            TempData["message"] = message;

            return RedirectToAction("Index","Home");
        }

        }
    }
