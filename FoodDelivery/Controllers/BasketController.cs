using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using FoodDelivery.ViewModels;

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
        var customer = GetCustomer(userId).Result;

        if (customer != null)
        {
            var orderProducts = _context.OrderProducts
                .Include(op => op.Product)
                .Where(op => op.CustomerId == customer.CustomerId && op.OrderId == null)
                .ToList();

            return View(orderProducts);
        }
        else
        {
            return Redirect("/Identity/Account/Login");
        }
    }

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

        var existingOrderProduct = _context.OrderProducts
            .Include(op => op.Product)
            .FirstOrDefault(op => op.CustomerId == customer.CustomerId && op.OrderId == null && op.ProductId == productId);

        if (existingOrderProduct != null)
        {
            // Якщо продукт вже є в кошику, збільшуємо кількість на 1
            existingOrderProduct.Quantity += 1;
            existingOrderProduct.TotalPrice = existingOrderProduct.Quantity * product.Price;
            existingOrderProduct.TotalWeight = existingOrderProduct.Quantity * product.Netto;
        }
        else
        {
            var orderProduct = new OrderProduct
            {
                Quantity = quantity,
                TotalPrice = quantity * product.Price,
                TotalWeight = quantity * product.Netto,
                ProductId = productId,
                CustomerId = customer.CustomerId
            };

            _context.OrderProducts.Add(orderProduct);
        }

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
    [HttpPost]
    public async Task<IActionResult> Order()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var customer = await GetCustomer(userId);
        var orderProducts = await _context.OrderProducts
            .Include(op => op.Product)
            .Where(op => op.CustomerId == customer.CustomerId && op.OrderId == null)
            .ToListAsync();

        var totalOrderPrice = orderProducts.Sum(op => op.TotalPrice);

        var orderViewModel = new OrderViewModel
        {
            OrderProducts = orderProducts.Select(op => new OrderProductViewModel
            {
                ProductId = op.ProductId,
                Name = op.Product.Name,
                Price = op.Product.Price,
                ImageUrl = op.Product.Image,
                Quantity = op.Quantity,
                TotalPrice = op.TotalPrice
            }).ToList(),
            TotalPrice = totalOrderPrice
        };

        // Store the OrderViewModel in TempData
        TempData["OrderViewModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(orderViewModel);

        // Redirect to the OrderController
        return RedirectToAction("Index", "Order");
    }

    private async Task<Customer> GetCustomer(string Id)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(c => c.ApplicationUserId == Id);

        return customer;
    }
}
