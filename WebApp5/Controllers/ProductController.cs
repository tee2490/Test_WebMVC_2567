using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public IActionResult Create(ProductDto productDto,IFormFile file)
        {
            return View();
        }
    }
}
