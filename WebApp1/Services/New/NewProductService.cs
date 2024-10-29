
using WebApp1.Data;

namespace WebApp1.Services.New
{
    public class NewProductService : INewProductService
    {
        private readonly DataContext db;

        public NewProductService(DataContext db)
        {
            this.db = db;
        }

        public List<Product> GetAll()
        {
          return  db.Products.ToList();
        }
    }
}
