
using WebApp5.Data;

namespace WebApp5.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext db;

        public ProductService(DataContext db)
        {
            this.db = db;
        }

        public Task Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetAll()
        {
            return await db.Products.Include(px=>px.Category).ToListAsync();
        }

        public Task<Product> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
