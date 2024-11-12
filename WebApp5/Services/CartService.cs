
namespace WebApp5.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext db;
        private readonly ShoppingCartService shoppingCartService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private ShoppingCartDto shoppingCartDto ;

        public CartService(DataContext db,ShoppingCartService shoppingCartService,IWebHostEnvironment webHostEnvironment)
        {
            this.db = db;
            this.shoppingCartService = shoppingCartService;
            this.webHostEnvironment = webHostEnvironment;
            shoppingCartDto = new ShoppingCartDto();
        }

        public async Task<ShoppingCartDto> GetAll(string userId)
        {
            shoppingCartDto.ListCart = await db.ShoppingCarts.Include(x => x.Product)
                                            .Where(u => u.UserId.Equals(userId)).ToListAsync();

            foreach (var cart in  shoppingCartDto.ListCart)
            {
                shoppingCartDto.OrderHeader.OrderTotal += cart.Product.Price * cart.Count;
            }

            return shoppingCartDto;

        }

        public async Task Minus(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);

            if (cart.Count > 1)
            {
                shoppingCartService.DecrementCount(cart, 1);
                await shoppingCartService.Save();
            }

        }

        public async Task Plus(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);
            shoppingCartService.IncrementCount(cart, 1);
            await shoppingCartService.Save();
        }

        public async Task Remove(int cartId)
        {
            var cart = await db.ShoppingCarts.FindAsync(cartId);

            if (cart != null)
            {
                db.Remove(cart);
                await shoppingCartService.Save();
            }

        }

        public async Task<ShoppingCartDto> Summary(string userId)
        {
            shoppingCartDto.ListCart = await db.ShoppingCarts.Include(x => x.Product)
                                                .Where(u => u.UserId.Equals(userId)).ToListAsync();

            shoppingCartDto.OrderHeader.User = await db.Users.FindAsync(userId);

            shoppingCartDto.OrderHeader.Name = shoppingCartDto.OrderHeader.User.FullName;
            shoppingCartDto.OrderHeader.StreetAddress = shoppingCartDto.OrderHeader.User.StreetAddress;
            shoppingCartDto.OrderHeader.City = shoppingCartDto.OrderHeader.User.City;
            shoppingCartDto.OrderHeader.State = shoppingCartDto.OrderHeader.User.State;
            shoppingCartDto.OrderHeader.PostalCode = shoppingCartDto.OrderHeader.User.PostalCode;

            foreach (var cart in shoppingCartDto.ListCart)
            {
                shoppingCartDto.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return shoppingCartDto;

        }

        public async Task<bool> SummaryPost(ShoppingCartDto shoppingCartDto,string userId,IFormFile file)
        {
            shoppingCartDto.ListCart = await db.ShoppingCarts.Include(x => x.Product)
                .Where(u => u.UserId.Equals(userId)).ToListAsync();

            shoppingCartDto.OrderHeader.UserId = userId;
            shoppingCartDto.OrderHeader.PaymentDate = DateTime.Now;
            shoppingCartDto.OrderHeader.OrderStatus = SD.StatusPending; //รอการตรวจสอบ

            foreach (var cart in shoppingCartDto.ListCart)
            {
                shoppingCartDto.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            var user = await db.Users.FindAsync(userId);

            #region Image Management
            string wwwRootPath = webHostEnvironment.WebRootPath;
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(file.FileName);
                var uploads = Path.Combine(wwwRootPath, @"images\payments");

                if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);

                //บันทึกรุปภาพใหม่
                using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                shoppingCartDto.OrderHeader.PaymentImage = @"\images\payments\" + fileName + extension;
            }
            #endregion

            await db.OrderHeaders.AddAsync(shoppingCartDto.OrderHeader);
            await db.SaveChangesAsync();

            foreach (var cart in shoppingCartDto.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = shoppingCartDto.OrderHeader.Id,
                    Count = cart.Count
                };

                await db.OrderDetails.AddAsync(orderDetail);
            }

            db.ShoppingCarts.RemoveRange(shoppingCartDto.ListCart); //ลบตะกร้า
            var success = await db.SaveChangesAsync() > 0;

            return success;

        }
    }
}
