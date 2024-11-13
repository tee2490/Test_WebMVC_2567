

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

        public async Task<dynamic> DeleteOrder(int orderId)
        {
            var orderHeaderFromDb = await db.OrderHeaders.FindAsync(orderId);

            if (orderHeaderFromDb == null) return "Not found";

            #region Image Management ลบรูปภาพบิลชำระเงิน
            string wwwRootPath = webHostEnvironment.WebRootPath;

            if (orderHeaderFromDb.PaymentImage != null)
            {
                var oldImagePath = Path.Combine(wwwRootPath, orderHeaderFromDb.PaymentImage.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            #endregion

            db.Remove(orderHeaderFromDb); //ถ้าลบฝั่ง 1 ฝั่ง many ลบอัตโนมัติ
            var success = await db.SaveChangesAsync() > 0;

            //var message = success ? "Success" : "Not success";

            return success ? "Success" : "Not success";

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

        public async Task<bool> UpdateOrderHeader(OrderDto orderDto)
        {
            var orderHeaderFromDb = await db.OrderHeaders.FindAsync(orderDto.OrderHeader.Id);

            orderHeaderFromDb.Name = orderDto.OrderHeader.Name;
            orderHeaderFromDb.StreetAddress = orderDto.OrderHeader.StreetAddress;
            orderHeaderFromDb.City = orderDto.OrderHeader.City;
            orderHeaderFromDb.State = orderDto.OrderHeader.State;
            orderHeaderFromDb.PostalCode = orderDto.OrderHeader.PostalCode;

            db.OrderHeaders.Update(orderHeaderFromDb);
            var success = await db.SaveChangesAsync() > 0;

            return success;

        }

        public async Task<string> UpdateStatusOrder(OrderDto orderDto, string status)
        {
            string message;
            var orderHeaderFromDb = await db.OrderHeaders.FindAsync(orderDto.OrderHeader.Id);

            if (orderHeaderFromDb.OrderStatus == SD.StatusPending)
            {
                orderHeaderFromDb.OrderStatus = status;
                await db.SaveChangesAsync();
                message = "Status has been updated Succesfully.";
            }
            else
            {
                message = "Can't update because status has ended.";
            }

            return message;

        }
    }
}
