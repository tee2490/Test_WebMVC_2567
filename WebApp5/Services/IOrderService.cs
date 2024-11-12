namespace WebApp5.Services
{
    public interface IOrderService
    {
        Task<List<OrderHeader>> GetAllOrderHeader();
    }
}
