using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class CourierController : Controller
{
    private readonly ApplicationDbContext _context;

    public CourierController(ApplicationDbContext context)
    {
        _context = context;
    }

    [Authorize] // Додано атрибут для забезпечення доступу тільки для автентифікованих користувачів
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var courier = await GetCourier(userId);

        if (courier != null)
        {
            // Fetch orders associated with the delivery list
            var availableOrders = _context.Orders
                .Include(order => order.Customers)
                .Include(order => order.DeliveryList)
                .ThenInclude(deliveryList => deliveryList.CourierInfo)
                .Where(order => order.DeliveryList == null)
                .ToList();

            ViewData["Courier"] = courier; // Pass courier info to the view

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
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var courier = await GetCourier(userId);
        var order = await _context.Orders
            .Include(o => o.Customers)
            .Include(o => o.DeliveryList)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        // Verify that the CourierId is assigned correctly
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
            // Create a new courier if not found
            courier = new CourierInfo
            {
                ApplicationUserId = userId,
                // Add any other properties you want to initialize
            };

            _context.CourierInfos.Add(courier);
            await _context.SaveChangesAsync();
        }

        return courier;
    }
}
