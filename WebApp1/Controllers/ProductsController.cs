using Microsoft.AspNetCore.Mvc;
using WebApp1.Services;

namespace WebApp1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService ps;

        public ProductsController(IProductService ps)
        {
            this.ps = ps;
        }

        public IActionResult Index()
        {
            return View(ps.GetProducts());
        }

        public IActionResult Delete(int id)
        {
            ps.DeleteData(id);

            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] //attribute คุณสมบัติ
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                //ถูกต้อง
               var exist =  ps.AddData(product);

                if(!exist)return RedirectToAction("Index");

                TempData["alert"] = "คุณตั้งรหัสซ้ำ";
            }
            
            return View();
        }

    }
}

//Protocol ข้อตกลง
//post เพิ่ม
//put  แก้ไข
//delete ลบ
//get อ่าน