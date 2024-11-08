using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp5.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var category = await categoryService.GetCategories();
            return View(category);
        }

        public async Task<IActionResult> Create(int id) 
        {
            if (id != 0) 
            {
                //update
              var result = await categoryService.Find(id);

                if (result == null) RedirectToAction(nameof(Index));

                return View(result);
            }

            return View(new Category());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Id != 0)
            {
                //update
                await categoryService.Update(category);
            }
            else
            {
                //create
             var success = await categoryService.Add(category);
                if (success) TempData["message"] = "เพิ่มข้อมูลสำเร็จ";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
