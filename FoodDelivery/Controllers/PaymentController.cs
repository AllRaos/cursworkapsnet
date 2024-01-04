using Microsoft.AspNetCore.Mvc;

public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ProcessPayment(string cardNumber, string cardHolder, string expirationDate, string cvv)
    {
        // Implement payment processing logic here
        // This is a placeholder; replace it with actual payment processing logic

        // For demonstration purposes, redirect to a thank you page
        return RedirectToAction("ThankYou");
    }

    public IActionResult ThankYou()
    {
        // Display a thank you message or any other relevant information
        return View();
    }
}
