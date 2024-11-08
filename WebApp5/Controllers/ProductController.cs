using Microsoft.AspNetCore.Mvc;
using WebApp5.Data;

namespace WebApp5.Controllers
{
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

        public IActionResult Create(int? id) 
        {
                    

            if (id == null || id == 0)
            {
                //create
            }
            else
            {
                //productVM.Product = productContext.Products.Find(id);
            }

            return View(ps.ProductDto());
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
    }
}
