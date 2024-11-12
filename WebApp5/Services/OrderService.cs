
namespace WebApp5.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext db;
        private readonly IWebHostEnvironment webHostEnvironment;

        public OrderService(DataContext db,IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<OrderHeader>> GetAllOrderHeader()
        {
            return await db.OrderHeaders.ToListAsync();
        }
    }
}
