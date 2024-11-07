namespace WebApp5.Services
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories();
        Task Add(Category category);
        Task Update(Category category);
        Task Delete(int id);
        Task<Category> Find(int id);
    }
}
