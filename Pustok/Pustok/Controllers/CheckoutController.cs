using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class CheckoutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
