using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class OrderController : Controller
{
    public IActionResult Index()
    {
        // Retrieve the OrderViewModel from TempData
        var orderViewModelJson = TempData["OrderViewModel"] as string;

        if (string.IsNullOrEmpty(orderViewModelJson))
        {
            // Handle the scenario where there are no order products
            return View(new OrderViewModel { OrderProducts = new List<OrderProductViewModel>(), TotalPrice = 0 });
        }

        // Deserialize the JSON string to OrderViewModel
        var orderViewModelObject = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderViewModel>(orderViewModelJson);

        return View(orderViewModelObject);
    }
}
