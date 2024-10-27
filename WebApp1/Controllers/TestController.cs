using Microsoft.AspNetCore.Mvc;

namespace WebApp1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Show()
        {
            int[] data = [1,2,3,4,5,6];
           

            return View(data);
        }
    }
}
