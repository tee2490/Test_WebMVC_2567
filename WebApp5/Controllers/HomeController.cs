using Microsoft.AspNetCore.Mvc;

namespace WebApp5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService ps;

        public HomeController(ProductService ps)
        {
            this.ps = ps;
        }

        public async Task<IActionResult> Index()
        {
            return View( await ps.GetAll());
        }

 
    }
}
