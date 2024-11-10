
using System.Security.Claims;

namespace WebApp5.Services
{
    public class ShoppingCartService : IShoppingCartService<ShoppingCart>
    {
        private readonly DataContext db;

        public ShoppingCartService(DataContext productContext)
        {
            db = productContext;
        }

        public void DecrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
        }

        public void IncrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
        }

        public async Task Save()
        {
          await  db.SaveChangesAsync();
        }

        public async Task Add(ShoppingCart shoppingCart)
        {
            //สินค้าตัวนี้มียัง
            ShoppingCart cartFromDb = db.ShoppingCarts.FirstOrDefault(
                u => u.UserId == shoppingCart.UserId && u.ProductId == shoppingCart.ProductId);

            if (cartFromDb == null) //สินค้าใหม่
            {
                await db.AddAsync(shoppingCart);
                
            }
            else //สินค้าเก่า
            {
                IncrementCount(cartFromDb, shoppingCart.Count);
            }

            await Save();

        }

        public async Task<ShoppingCart> Detail(int id)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = id,
                Product = await db.Products.Include(x => x.Category)
                                         .FirstOrDefaultAsync(x => x.Id.Equals(id)),
            };

            return cartObj;

        }
    }

}
