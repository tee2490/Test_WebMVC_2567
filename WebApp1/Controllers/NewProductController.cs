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

        public IActionResult Index(string keyword)
        {
            var products = ns.GetAll(keyword);
            return View(products);
        }

        public IActionResult UpCreate(int? id)
        {
            if (id == null)
            {
                //addd
            }
            else
            {
                //update
                var product = ns.SearchData(id.Value);
                if (product != null)
                {
                    return View(product);
                }
                return RedirectToAction("Index");

            }

            return View(new Product());
        }

        [HttpPost]
        public IActionResult UpCreate(Product product,IFormFile file)
        {
            if (product.Id == 0)
                ns.AddData(product,file);
            else 
                ns.UpdateData(product, file);

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            ns.DeleteData(id);
            return RedirectToAction("Index");
        }
        


    }
}
