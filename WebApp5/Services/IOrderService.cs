namespace WebApp5.Services
{
    public interface IOrderService
    {
        Task<List<OrderHeader>> GetAllOrderHeader();
        Task<OrderDto> GetOrderDetail(int orderId);
    }
}
