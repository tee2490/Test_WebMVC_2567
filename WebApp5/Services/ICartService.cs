namespace WebApp5.Services
{
    public interface ICartService
    {
        Task<ShoppingCartDto> GetAll(string userId);
    }
}
