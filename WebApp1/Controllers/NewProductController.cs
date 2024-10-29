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
        public IActionResult UpCreate(Product product)
        {
            if (product.Id == 0)
                ns.AddData(product);
            else 
                ns.UpdateData(product);

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            ns.DeleteData(id);
            return RedirectToAction("Index");
        }


    }
}
