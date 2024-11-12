namespace WebApp5.Models.Dtos
{
    public class ShoppingCartDto
    {
        public OrderHeader OrderHeader { get; set; } = new OrderHeader();
        public IEnumerable<ShoppingCart> ListCart { get; set; }

    }
}
