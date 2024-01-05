using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class CourierController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public CourierController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var courier = await GetCourier(user.Id);

        if (courier != null)
        {
            var availableOrders = _context.Orders
                .Include(order => order.Customers)
                .Include(order => order.DeliveryList)
                .ThenInclude(deliveryList => deliveryList.CourierInfo)
                .Where(order => order.DeliveryList == null)
                .ToList();

            ViewData["Courier"] = courier;

            return View(availableOrders);
        }
        else
        {
            return Redirect("/Identity/Account/Login");
        }
    }

    [HttpPost]
    public async Task<IActionResult> TakeOrder(int orderId)
    {
        var user = await _userManager.GetUserAsync(User);
        var courier = await GetCourier(user.Id);
        var order = await _context.Orders
            .Include(o => o.Customers)
            .Include(o => o.DeliveryList)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        order.DeliveryList = new DeliveryList { CourierId = courier.CourierId };
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task<CourierInfo> GetCourier(string userId)
    {
        var courier = await _context.CourierInfos
            .Include(c => c.DeliveryLists)
            .ThenInclude(dl => dl.Order)
            .ThenInclude(order => order.Customers)
            .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);

        if (courier == null)
        {
            courier = new CourierInfo
            {
                ApplicationUserId = userId,
            };

            _context.CourierInfos.Add(courier);
            await _context.SaveChangesAsync();
        }

        return courier;
    }
}
