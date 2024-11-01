using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp3.Controllers
{
    
    public class TestAuthorController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
