using Microsoft.AspNetCore.Mvc;

namespace WebApp5.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService ps;

        public ProductController(IProductService ps)
        {
            this.ps = ps;
        }

        public async Task<IActionResult> Index()
        {
            return View( await ps.GetAll() );
        }
    }
}
