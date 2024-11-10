namespace WebApp5.Models.Dtos
{
    public class ShoppingCartDto
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<ShoppingCart> ListCart { get; set; }

    }
}
