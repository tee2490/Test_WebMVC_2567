namespace WebApp5.Models.Dtos
{
    public class OrderDto
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
