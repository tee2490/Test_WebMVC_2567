namespace WebApp5.Services
{
    public interface IOrderService
    {
        Task<List<OrderHeader>> GetAllOrderHeader();
        Task<OrderDto> GetOrderDetail(int orderId);
        Task<bool> UpdateOrderHeader(OrderDto orderDto);
        Task<string> UpdateStatusOrder(OrderDto orderDto, string status);
        Task<dynamic> DeleteOrder(int orderId);
    }
}
