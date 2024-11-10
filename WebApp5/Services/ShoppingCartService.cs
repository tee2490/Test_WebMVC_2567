
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
          await  db.AddAsync(shoppingCart);
        }

    }

}
