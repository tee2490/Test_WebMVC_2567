
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public ProductDto ProductDto() 
        {
            ProductDto productDto = new()
            {
                Product = new() { Name = "TestProduct", Price = 1, Description = "Test Descript" },
                CategoryList = db.Categories.Select(
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.Id.ToString()
                   })
            };

            return productDto;
        }

    }
}
