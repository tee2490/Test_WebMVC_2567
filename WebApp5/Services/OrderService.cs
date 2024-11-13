

namespace WebApp5.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext db;
        private readonly IWebHostEnvironment webHostEnvironment;
        private OrderDto orderDto;

        public OrderService(DataContext db,IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<OrderHeader>> GetAllOrderHeader()
        {
            return await db.OrderHeaders.ToListAsync();
        }

        public async Task<OrderDto> GetOrderDetail(int orderId)
        {
            orderDto=new OrderDto()
            {
                OrderHeader = await db.OrderHeaders.Include(x => x.User)
                                .FirstOrDefaultAsync(x => x.Id.Equals(orderId)),
                OrderDetail = await db.OrderDetails.Include(x => x.Product)
                                .Where(x => x.OrderId.Equals(orderId)).ToListAsync()
            };

            return orderDto;

        } 
    }
}
