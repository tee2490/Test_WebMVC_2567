
using WebApp5.Data;

namespace WebApp5.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext db;

        public CategoryService(DataContext dataContext)
        {
            db = dataContext;
        }

        public async Task<bool> Add(Category category)
        {
           await db.Categories.AddAsync(category);
           var status = await db.SaveChangesAsync() > 0;
            return status;
        }

        public async Task Delete(int id)
        {
            var result = await Find(id);

            if (result != null) {
                db.Categories.Remove(result);
                db.SaveChanges();
            }
        }

        public async Task<Category> Find(int id)
        {
            return await db.Categories.AsNoTracking()
                .FirstAsync(px=>px.Id.Equals(id));
        }

        public async Task<List<Category>> GetCategories()
        {
            return await db.Categories.ToListAsync();
        }

        public async Task Update(Category category)
        {
            var result = await Find(category.Id);

            if (result != null) {
                db.Categories.Update(category);
                db.SaveChanges();
            }
        }
    }
}
