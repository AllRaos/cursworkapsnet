using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

public class PaymentController : Controller
{
    private readonly ApplicationDbContext _context;

    public PaymentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(string cardNumber, string cardHolder, string expirationDate, string cvv, string street, int house)
    {
        // Implement payment processing logic here
        // This is a placeholder; replace it with actual payment processing logic

        // Assuming payment is successful, proceed with placing the order
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = await GetCustomer(userId);
        var orderProducts = await _context.OrderProducts
            .Include(op => op.Product) // Include Product information if needed
            .Where(op => op.CustomerId == customer.CustomerId && op.OrderId == null)
            .ToListAsync();

        // Create an Order entity
        var order = new Order
        {
            CustomerId = customer.CustomerId,
            OrderProducts = orderProducts.ToList(),
            // Add other order details as needed
        };

        // Add the order to the database
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Update OrderId for the order products
        foreach (var op in orderProducts)
        {
            op.OrderId = order.OrderId;
        }

        // Update Customer with street and house information
        customer.Street = street;
        customer.House = house;
        await _context.SaveChangesAsync();

        // Redirect to a thank you page or another relevant page
        return RedirectToAction("ThankYou");
    }

    private async Task<Customer> GetCustomer(string Id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.ApplicationUserId == Id);

        return customer;
    }

    public IActionResult ThankYou()
    {
        // Display a thank you message or any other relevant information
        return View();
    }
}
