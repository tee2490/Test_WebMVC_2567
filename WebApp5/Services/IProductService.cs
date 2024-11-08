namespace WebApp5.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<bool> AddUpdate(ProductDto product,IFormFile file);
        Task<int> Delete(int id);
    }
}
