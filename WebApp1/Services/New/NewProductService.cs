
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
          return  db.Products.OrderByDescending(px=>px.Id).ToList();
        }

        public void AddData(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public Product SearchData(int id)
        {
          return db.Products.Find(id);
        }

        public void UpdateData(Product product)
        {
            db.Products.Update(product);
            db.SaveChanges();
        }

        public void DeleteData(int id)
        {
            var product = SearchData(id);

            if (product != null) 
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
