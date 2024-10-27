using WebApp1.Models;

namespace WebApp1.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        void GenData(int number);
        void DeleteData(int id);
        Product SearchProduct(int id);
        bool AddData(Product product);
        void UpdateData(Product product);

    }
}
