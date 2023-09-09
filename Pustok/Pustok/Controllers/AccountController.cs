using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pustok.Database;
using Pustok.Services.Abstracts;
using Pustok.ViewModels;

namespace Pustok.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly PustokDbContext _dbContext;
    private readonly IUserService _userService;

    public AccountController(PustokDbContext dbContext, IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Dashboard()
    {
        var user1 = _userService.CurrentUser;
        return View();
    }

    public IActionResult Index()
    {
        var total = CalculateCartTotal();
        var cartItems = GetCartItems();

        var model = new CartViewModel
        {
            CartItems = cartItems,
            Total = total
        };

        return View(model);
    }


    private List<CartItem> GetCartItems()
    {
        var cartItems = _cartService.GetCartItems();
        return cartItems;
    }


    private decimal CalculateCartTotal()
    {
        var cartItems = GetCartItems();
        decimal total = 0;
        foreach (var item in cartItems)
        {
            total += item.Price * item.Quantity;
        }
        return total;
    }


    public IActionResult Orders()
    {
        return View();
    }

    public IActionResult Addresses()
    {
        return View();
    }

    public IActionResult AccountDetails()
    {
        return View();
    }

    public IActionResult Logout()
    {
        return RedirectToAction("index", "home");
    }

}
