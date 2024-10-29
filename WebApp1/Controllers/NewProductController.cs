using Microsoft.AspNetCore.Mvc;
using WebApp1.Services.New;

namespace WebApp1.Controllers
{
    public class NewProductController : Controller
    {
        private readonly INewProductService ns;

        public NewProductController(INewProductService ns)
        {
            this.ns = ns;
        }

        public IActionResult Index()
        {
            return View(ns.GetAll());
        }
    }
}
