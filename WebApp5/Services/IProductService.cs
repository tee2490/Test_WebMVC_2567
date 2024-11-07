namespace WebApp5.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task Add(Product product);
    }
}
