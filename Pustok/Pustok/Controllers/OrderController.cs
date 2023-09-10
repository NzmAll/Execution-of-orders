using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]

public class OrderController : Controller
{
    public readonly IOrderService _orderService;
    private readonly IEmailService _emailService;

    public OrderController(IOrderService orderService, IEmailService emailService)
    {
        _orderService = orderService;
        _emailService = emailService;
    }

    // Sipariş statusunu güncellemek için bir aksiyon ekleyin
    public IActionResult UpdateOrderStatus(int orderId, string newStatus)
    {
        var order = _orderService.GetOrderById(orderId);
        var user = order.User;
        _emailService.SendOrderStatusEmail(user.Email, user.FirstName, user.LastName, order.OrderNumber, newStatus);

        return RedirectToAction("OrderDetails", new { orderId });
    }
}
