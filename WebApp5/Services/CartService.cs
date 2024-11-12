
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

        public async Task Minus(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);

            if (cart.Count > 1)
            {
                shoppingCartService.DecrementCount(cart, 1);
                await shoppingCartService.Save();
            }

        }

        public async Task Plus(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);
            shoppingCartService.IncrementCount(cart, 1);
            await shoppingCartService.Save();
        }

        public async Task Remove(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);

            if (cart != null)
            {
                db.Remove(cart);
                await shoppingCartService.Save();
            }

        }

        public async Task<ShoppingCartDto> Summary(string userId)
        {
            var shoppingCartDto = new ShoppingCartDto()
            {
                ListCart = await db.ShoppingCarts.Include(x => x.Product)
                                                .Where(u => u.UserId.Equals(userId)).ToListAsync(),
                OrderHeader = new()
            };

            shoppingCartDto.OrderHeader.User = await db.Users.FindAsync(userId);

            shoppingCartDto.OrderHeader.Name = shoppingCartDto.OrderHeader.User.FullName;
            shoppingCartDto.OrderHeader.StreetAddress = shoppingCartDto.OrderHeader.User.StreetAddress;
            shoppingCartDto.OrderHeader.City = shoppingCartDto.OrderHeader.User.City;
            shoppingCartDto.OrderHeader.State = shoppingCartDto.OrderHeader.User.State;
            shoppingCartDto.OrderHeader.PostalCode = shoppingCartDto.OrderHeader.User.PostalCode;

            foreach (var cart in shoppingCartDto.ListCart)
            {
                shoppingCartDto.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return shoppingCartDto;

        }
    }
}
