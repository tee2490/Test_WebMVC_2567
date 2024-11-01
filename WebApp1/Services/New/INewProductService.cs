namespace WebApp1.Services.New
{
    public interface INewProductService
    {
        List<Product> GetAll(string keyword);
        void AddData(Product product,IFormFile file); //ไฟล์เดียว
        Product SearchData(int id);
        void UpdateData(Product product,IFormFile file);
        void DeleteData(int id);

    }
}
