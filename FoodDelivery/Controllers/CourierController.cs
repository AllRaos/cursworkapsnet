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

    // Оновлений метод Index
    [Authorize(Roles = "Courier")]
    // Inside the Index action
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var courier = await GetCourier(user.Id);

        if (courier != null)
        {
            var allOrders = _context.Orders
                .Include(order => order.Customers)
                .Include(order => order.DeliveryList)
                .ToList();

            ViewData["Courier"] = courier;
            ViewData["DeliveryLists"] = _context.DeliveryLists.ToList();
            ViewData["Orders"] = allOrders;

            // Retrieve AspNetUsers Ids for customers and store them in ViewData
            var customerIds = allOrders
                .Where(o => o.Customers != null)
                .Select(o => o.Customers.ApplicationUserId)
                .ToList();

            var userManager = _userManager;
            var aspNetUserIds = customerIds.Select(id => userManager.FindByIdAsync(id).Result?.Id).ToList();
            ViewData["AspNetUserIds"] = aspNetUserIds;

            return View(allOrders);
        }
        else
        {
            return Redirect("/Identity/Account/Login");
        }
    }


    // Оновлений метод TakeOrder
    [HttpPost]
    public async Task<IActionResult> TakeOrder(int orderId)
    {
        var user = await _userManager.GetUserAsync(User);
        var courier = await GetCourier(user.Id);
        var order = await GetOrder(orderId);

        if (order == null)
        {
            return NotFound();
        }

        var deliveryList = new DeliveryList { Order = order, CourierId = courier.CourierId, Status = "доставляється" };
        _context.DeliveryLists.Add(deliveryList);
        await _context.SaveChangesAsync();

        // Перезавантаження сторінки
        return RedirectToAction(nameof(Index));
    }

    private async Task<CourierInfo> GetCourier(string userId)
    {
        var courier = await _context.CourierInfos
            .Include(c => c.DeliveryLists)
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
    private async Task<Order> GetOrder(int orderId)
    {
        return await _context.Orders
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    [HttpPost]
    public async Task<IActionResult> MarkOrderDelivered(int orderId)
    {
        var user = await _userManager.GetUserAsync(User);
        var courier = await GetCourier(user.Id);

        var order = await _context.Orders
            .Include(o => o.DeliveryList)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        // Перевірка, чи замовлення належить поточному кур'єру
        if (order.DeliveryList == null || order.DeliveryList.CourierId != courier.CourierId)
        {
            // This order does not belong to the current courier
            return BadRequest("Unable to mark the order as delivered.");
        }

        // Change the status to "доставлено" in the DeliveryList
        order.DeliveryList.Status = "доставлено";

        await _context.SaveChangesAsync();

        // Redirect to the Index action to refresh the view
        return RedirectToAction(nameof(Index));
    }
}