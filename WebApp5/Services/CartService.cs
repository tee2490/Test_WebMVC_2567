
using System.Security.Claims;

namespace WebApp5.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext db;
        private readonly ShoppingCartService shoppingCartService;

        public CartService(DataContext db,ShoppingCartService shoppingCartService)
        {
            this.db = db;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<ShoppingCartDto> GetAll(string userId)
        {
           var  shoppingCartDto = new ShoppingCartDto()
            {
                ListCart = await db.ShoppingCarts.Include(x => x.Product)
                                            .Where(u => u.UserId.Equals(userId)).ToListAsync(),
                OrderHeader = new()
            };

            foreach (var cart in  shoppingCartDto.ListCart)
            {
                shoppingCartDto.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
            }

            return shoppingCartDto;

        }

        public async Task Plus(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);
            shoppingCartService.IncrementCount(cart, 1);
            await shoppingCartService.Save();
        }

    }
}
