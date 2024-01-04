using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

public class BasketController : Controller
{
    private readonly ApplicationDbContext _context;

    public BasketController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Basket
    [HttpGet]
    public IActionResult Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = GetCustomer(userId).Result; // Використовуйте Result, а не await

        if (customer != null)
        {
            var orderProducts = _context.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.CustomerId == customer.CustomerId && op.OrderId == null)
                .ToList(); // Використовуйте ToList() замість ToListAsync()

            return View(orderProducts);
        }
        else
        {
            // Обробити випадок, коли користувача не знайдено
            // Наприклад, повернути користувача на сторінку входу або показати повідомлення
            return Redirect("/Identity/Account/Login");
        }
    }

    // POST: Basket/AddToCart
    [HttpPost]
    public async Task<IActionResult> Add(int productId, int quantity)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = await GetCustomer(userId);
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        var orderProduct = new OrderProduct
        {
            Quantity = quantity,
            TotalPrice = quantity * product.Price,
            TotalWeight = quantity * product.Netto,
            ProductId = productId,
            CustomerId = customer.CustomerId
        };

        _context.OrderProducts.Add(orderProduct);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    // POST: Basket/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(int orderProductId)
    {
        var orderProduct = await _context.OrderProducts.FindAsync(orderProductId);

        if (orderProduct == null)
        {
            return NotFound();
        }

        _context.OrderProducts.Remove(orderProduct);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // POST: Basket/Order
    [HttpPost]
    public async Task<IActionResult> Order()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = await GetCustomer(userId);
        var orderProducts = await _context.OrderProducts
            .Where(op => op.CustomerId == customer.CustomerId && op.OrderId == null)
            .ToListAsync();

        // Assuming you have an Order entity and logic to create an order here.
        var order = new Order
        {
            CustomerId = customer.CustomerId,
            OrderProducts = orderProducts.ToList(),
            // Add other order details as needed
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Clear the items from the basket after placing the order
        foreach (var op in orderProducts)
        {
            op.OrderId = order.OrderId;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Order");
    }

    private async Task<Customer> GetCustomer(string Id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.ApplicationUserId == Id);

        return customer;
    }
}
