using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp5.Data;

namespace WebApp5.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ProductController : Controller
    {
        private readonly ProductService ps;
        private readonly DataContext dataContext;

        public ProductController(ProductService ps,DataContext dataContext)
        {
            this.ps = ps;
            this.dataContext = dataContext;
        }

        public async Task<IActionResult> Index()
        {
            return View( await ps.GetAll() );
        }

        public async Task<IActionResult> Create(int? id) 
        {
            var pd = ps.ProductDto();

            if (id == null || id == 0)
            {
                //create
            }
            else
            {
              pd.Product = await ps.GetById(id.Value);
            }

            return View(pd);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto,IFormFile file)
        {
            ModelState.Remove("file"); //ยกเลิกการตรวจสอบบางฟิลด์

            if (ModelState.IsValid)
            {
              var success = await  ps.AddUpdate(productDto,file);

                if (success)
                {
                    TempData["message"] = "Product managed successfully";
                    return RedirectToAction(nameof(Index));
                }

            }

            return View(productDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var status = await ps.Delete(id);

            var message = status switch
            {
                0 => "ไม่พบข้อมูล",
                1 => "สำเร็จ",
                2 => "ไม่สำเร็จ",
            };

            TempData["message"] = message;

            return RedirectToAction(nameof(Index));
        }
    }
}
