using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
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


        [HttpPost]
         public async Task<IActionResult> UpdateOrderHeader()
        {
            var success = await orderService.UpdateOrderHeader(orderDto);

            string message = "Not Success";
            if (success) message = "Success";
            TempData["message"] = message;

            return RedirectToAction("Detail", "Order", new { orderId=orderDto.OrderHeader.Id });
        }


        [HttpPost]
        public async Task<IActionResult> StatusOrder(string status)
        {
            var message = await orderService.UpdateStatusOrder(orderDto, status);

            TempData["message"] = message;

            return RedirectToAction("Detail", "Order", new { orderId = orderDto.OrderHeader.Id });
        }


        public async Task<IActionResult> Delete(int orderId)
        {
            var message = await orderService.DeleteOrder(orderId);

            TempData["message"] = message;

            return RedirectToAction(nameof(Index));
        }



    }
}
