using Microsoft.AspNetCore.Mvc;
using WebApp4.Services;

namespace WebApp4.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;

        public RoleController(IRoleService roleService)
        {
            roleService = roleService;
            this.roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await roleService.GetAll();
            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

    }
}
