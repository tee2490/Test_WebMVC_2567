namespace WebApp5.Services
{
    public interface IShoppingCartService<T> //รับค่าใดๆ ก็ได้
    {
        void IncrementCount(T shoppingCart, int count);
        void DecrementCount(T shoppingCart, int count);

        Task Save();
        Task Add(T shoppingCart);

    }


}
