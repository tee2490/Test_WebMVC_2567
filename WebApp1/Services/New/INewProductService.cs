namespace WebApp1.Services.New
{
    public interface INewProductService
    {
        List<Product> GetAll(string keyword);
        void AddData(Product product);
        Product SearchData(int id);
        void UpdateData(Product product);
        void DeleteData(int id);

    }
}
