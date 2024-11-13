using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp5.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly OrderService orderService;

        [BindProperty]
        public OrderDto? orderDto { get; set; }

        public OrderController(OrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            return View( await orderService.GetAllOrderHeader());
        }


        public async Task<IActionResult> Detail(int orderId)
        {
            var orderDto = await orderService.GetOrderDetail(orderId);

            return View(orderDto);
        }

    }
}
