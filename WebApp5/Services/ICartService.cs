namespace WebApp5.Services
{
    public interface ICartService
    {
        Task<ShoppingCartDto> GetAll(string userId);
        Task Plus(int cartId);
        Task Minus(int cartId);
        Task Remove(int cartId);
        Task<ShoppingCartDto> Summary(string userId);
        Task<bool> SummaryPost(ShoppingCartDto shoppingCartDto,string userId,IFormFile file);
    }
}
