using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.Data;
using FoodDelivery.Models;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            var viewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                ImageUrl = product.Image,
                // Map other properties as needed
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ProductViewModel();
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Image != null)
                {
                    // Map ViewModel to Model
                    var model = new Product
                    {
                        Name = vm.Name,
                        Price = vm.Price,
                        Category = vm.Category,
                        Netto = vm.Netto,
                        Status = vm.Status,
                        // Map other properties as needed
                    };

                    // Save the image file to the wwwroot/images folder
                    string savePath = "/Images/Products/";
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + vm.Image.FileName;
                    string imagePath = Path.Combine(wwwRootPath + savePath, fileName);
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        await vm.Image.CopyToAsync(fileStream);
                    }

                    // Set the Image property in the Product model
                    model.Image = fileName;

                    // Add the new Product to the database
                    _context.Products.Add(model);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Add a validation error if no image is provided
                    ModelState.AddModelError("Image", "Please select an image.");
                    return View(vm);
                }
            }

            // If ModelState is not valid, reload the page with the existing model
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing product from the database
                    var existingProduct = await _context.Products.FindAsync(model.ProductId);

                    if (existingProduct == null)
                    {
                        return NotFound();
                    }

                    // Update the properties of the existing product with the new values
                    existingProduct.Name = model.Name;
                    existingProduct.Price = model.Price;
                    existingProduct.Category = model.Category;
                    existingProduct.Netto = model.Netto;
                    existingProduct.Status = model.Status;

                    // Check if a new image is provided
                    if (model.Image != null)
                    {
                        // Save the new image file to the wwwroot/Images/Products folder
                        string savePath = Path.Combine("Images", "Products");
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                        string imagePath = Path.Combine(wwwRootPath, savePath, fileName);

                        using (var fileStream = new FileStream(imagePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        // Delete the existing image file
                        if (!string.IsNullOrEmpty(existingProduct.Image))
                        {
                            var existingImagePath = Path.Combine(wwwRootPath, existingProduct.Image.TrimStart('/'));
                            System.IO.File.Delete(existingImagePath);
                        }

                        // Set the Image property in the existing product
                        existingProduct.Image = fileName;
                    }

                    // Update the product in the database
                    _context.Update(existingProduct);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }

            // If ModelState is not valid, reload the page with the existing model
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed
            };

            return View(viewModel);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
                Netto = product.Netto,
                Status = product.Status,
                // Map other properties as needed   
            };

            return View(viewModel);
        }

    }
}